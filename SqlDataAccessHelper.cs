using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Data.Common;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System.Configuration;

namespace AB.DataAccessLayer
{
    public class SQLDataAccessHelper
    {
        public bool traceLog;
        public string storedProcedure;

        public SQLDataAccessHelper()
        {

        }

        protected enum AdoConnectionOwnership
        {
            /// <summary>Connection is owned and managed by ADOHelper</summary>
            Internal,
            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        #region Declare members
        // necessary for handling the general case of needing event handlers for RowUpdating/ed events
        /// <summary>
        /// Internal handler used for bubbling up the event to the user
        /// </summary>
        protected RowUpdatingHandler m_rowUpdating;
        /// <summary>
        /// Internal handler used for bubbling up the event to the user
        /// </summary>
        protected RowUpdatedHandler m_rowUpdated;
        #endregion

        #region Delegates
        // also used in our general case of RowUpdating/ed events
        /// <summary>
        /// Delegate for creating a RowUpdatingEvent handler
        /// </summary>
        /// <param name="obj">The object that published the event</param>
        /// <param name="e">The RowUpdatingEventArgs for the event</param>
        public delegate void RowUpdatingHandler(object obj, RowUpdatingEventArgs e);

        /// <summary>
        /// Delegate for creating a RowUpdatedEvent handler
        /// </summary>
        /// <param name="obj">The object that published the event</param>
        /// <param name="e">The RowUpdatedEventArgs for the event</param>
        public delegate void RowUpdatedHandler(object obj, RowUpdatedEventArgs e);
        #endregion

        public static void DeriveParameters(SqlCommand cmd)
        {
            DeriveParameters(cmd);
        }

        #region overrides
        /*
						 * 
						 * */

        /// <summary>
        /// Returns an array of SqlParameters of the specified size
        /// </summary>
        /// <param name="size">size of the array</param>
        /// <returns>The array of SqlParameters</returns>
        public IDataParameter[] GetDataParameters(int size)
        {
            return new SqlParameter[size];
        }
        /// <summary>
        /// Returns a SqlConnection object for the given connection string
        /// </summary>
        /// <param name="connectionString">The connection string to be used to create the connection</param>
        /// <returns>A SqlConnection object</returns>
        public IDbConnection GetConnection()
        {
            //return new SqlConnection();
            string connectionString = AB.Utilities.Utilities.getValMessageforConnection("connString");
            //SqlConnection conn=new SqlConnection(connectionString);
            //		conn.Open();
            return new SqlConnection(connectionString);
            //return null;
            //return conn;
        }

        /// <summary>
        /// Returns a SqlDataAdapter object
        /// </summary>
        /// <returns>The SqlDataAdapter</returns>
        public IDbDataAdapter GetDataAdapter()
        {
            return new SqlDataAdapter();
        }
        /// <summary>
        /// Calls the CommandBuilder.DeriveParameters method for the specified provider, doing any setup and cleanup necessary
        /// </summary>
        /// <param name="cmd">The IDbCommand referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the Parameters collection of the IDbCommand. </param>
        public void DeriveParameters(IDbCommand cmd)
        {
            bool mustCloseConnection = false;

            if (!(cmd is SqlCommand))
                throw new ArgumentException("The command provided is not a SqlCommand instance.", "cmd");

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
                mustCloseConnection = true;
            }

            SqlCommandBuilder.DeriveParameters((SqlCommand)cmd);


            if (mustCloseConnection)
            {
                cmd.Connection.Close();
            }
        }


        /// <summary>
        /// Returns a SqlParameter object
        /// </summary>
        /// <returns>The SqlParameter object</returns>
        public IDataParameter GetParameter()
        {
            return new SqlParameter();
        }


        private int GetParameterSize(string name)
        {
            int Size = 255;
            return Size;
        }

        /// <summary>
        /// Detach the IDataParameters from the command object, so they can be used again.
        /// </summary>
        /// <param name="command">command object to clear</param>

        /// <summary>
        /// This cleans up the parameter syntax for an SQL Server call.  This was split out from PrepareCommand so that it could be called independently.
        /// </summary>
        /// <param name="command">An IDbCommand object containing the CommandText to clean.</param>
        public void CleanParameterSyntax(IDbCommand command)
        {
            // do nothing for SQL
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the provided SqlConnection. 
        /// </summary>
        /// <example>
        /// <code>
        /// XmlReader r = helper.ExecuteXmlReader(command);
        /// </code></example>
        /// <param name="command">The IDbCommand to execute</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(IDbCommand command)
        {
            bool mustCloseConnection = false;

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
                mustCloseConnection = true;
            }

            CleanParameterSyntax(command);
            // Create the DataAdapter & DataSet
            XmlReader retval = ((SqlCommand)command).ExecuteXmlReader();

            // Detach the SqlParameters from the command object, so they can be used again
            // don't do this...screws up output parameters
            // cmd.Parameters.Clear();

            if (mustCloseConnection)
            {
                command.Connection.Close();
            }

            return retval;
        }

        /// <summary>
        /// Provider specific code to set up the updating/ed event handlers used by UpdateDataset
        /// </summary>
        /// <param name="dataAdapter">DataAdapter to attach the event handlers to</param>
        /// <param name="rowUpdatingHandler">The handler to be called when a row is updating</param>
        /// <param name="rowUpdatedHandler">The handler to be called when a row is updated</param>
        protected void AddUpdateEventHandlers(IDbDataAdapter dataAdapter, RowUpdatingHandler rowUpdatingHandler, RowUpdatedHandler rowUpdatedHandler)
        {
            if (rowUpdatingHandler != null)
            {
                this.m_rowUpdating = rowUpdatingHandler;
                ((SqlDataAdapter)dataAdapter).RowUpdating += new SqlRowUpdatingEventHandler(RowUpdating);
            }

            if (rowUpdatedHandler != null)
            {
                this.m_rowUpdated = rowUpdatedHandler;
                ((SqlDataAdapter)dataAdapter).RowUpdated += new SqlRowUpdatedEventHandler(RowUpdated);
            }
        }

        /// <summary>
        /// Handles the RowUpdating event
        /// </summary>
        /// <param name="obj">The object that published the event</param>
        /// <param name="e">The SqlRowUpdatingEventArgs</param>
        protected void RowUpdating(object obj, SqlRowUpdatingEventArgs e)
        {
            if (this.m_rowUpdating != null)
                m_rowUpdating(obj, e);
        }

        /// <summary>
        /// Handles the RowUpdated event
        /// </summary>
        /// <param name="obj">The object that published the event</param>
        /// <param name="e">The SqlRowUpdatedEventArgs</param>
        protected void RowUpdated(object obj, SqlRowUpdatedEventArgs e)
        {
            if (this.m_rowUpdated != null)
                m_rowUpdated(obj, e);
        }

        /// <summary>
        /// Handle any provider-specific issues with BLOBs here by "washing" the IDataParameter and returning a new one that is set up appropriately for the provider.
        /// </summary>
        /// <param name="connection">The IDbConnection to use in cleansing the parameter</param>
        /// <param name="p">The parameter before cleansing</param>
        /// <returns>The parameter after it's been cleansed.</returns>
        protected IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p)
        {
            // do nothing special for BLOBs...as far as we know now.
            return p;
        }

        #endregion

        #region private utility methods
        /// <summary>
        /// This method is used to attach array of IDataParameters to an IDbCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of IDataParameterParameters to be added to command</param>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public void AttachParameters(IDbCommand command, IDataParameter[] commandParameters)
        {
            StringBuilder parameterDetails = new StringBuilder();
            string paramValue = String.Empty;
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                parameterDetails.Append("Stored Procedure : " + storedProcedure + Environment.NewLine + Environment.NewLine + "\t");
                foreach (IDataParameter p in commandParameters)
                {
                    #region For storing parameter details
                    if (p.Value != null)
                    {
                        paramValue = p.Value.ToString();
                    }
                    else
                    {
                        paramValue = "null";
                    }

                    //paramValue =p.Value == string.Empty  ? "":p.Value.ToString();
                    //parameterDetails.Append("Stored Procedure : "+ storedProcedure + Environment.NewLine + "\t" +
                    parameterDetails.Append("Parameter Name : " + p.ParameterName + Environment.NewLine + "\t" +
                        "Parameter Direction : " + p.Direction.ToString() + Environment.NewLine + "\t" +
                        "Parameter Data Type : " + p.DbType.ToString() + Environment.NewLine + "\t" +
                        "Parameter Value : " + paramValue + Environment.NewLine + Environment.NewLine + "\t");



                    #endregion

                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        if (p.DbType == DbType.Binary)
                        {
                            // special handling for BLOBs
                            command.Parameters.Add(GetBlobParameter(command.Connection, p));
                        }
                        else
                        {
                            command.Parameters.Add(p);
                        }
                    }
                }
                //Utility.LogTrace(traceLog,parameterDetails.ToString());
            }
            parameterDetails = null;
        }

        /// <summary>
        /// This method assigns dataRow column values to an IDataParameterCollection
        /// </summary>
        /// <param name="commandParameters">The IDataParameterCollection to be assigned values</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the parameter names are invalid.</exception>
        protected internal void AssignParameterValues(IDataParameterCollection commandParameters, DataRow dataRow)
        {
            if (commandParameters == null || dataRow == null)
            {
                // Do nothing if we get no data
                return;
            }

            DataColumnCollection columns = dataRow.Table.Columns;

            int i = 0;
            // Set the parameters values
            foreach (IDataParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null ||
                    commandParameter.ParameterName.Length <= 1)
                    throw new InvalidOperationException(string.Format(
                        "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                        i, commandParameter.ParameterName));

                if (columns.Contains(commandParameter.ParameterName))
                    commandParameter.Value = dataRow[commandParameter.ParameterName];
                else if (columns.Contains(commandParameter.ParameterName.Substring(1)))
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];

                i++;
            }
        }

        /// <summary>
        /// This method assigns dataRow column values to an array of IDataParameters
        /// </summary>
        /// <param name="commandParameters">Array of IDataParameters to be assigned values</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the parameter names are invalid.</exception>
        protected void AssignParameterValues(IDataParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                // Do nothing if we get no data
                return;
            }

            DataColumnCollection columns = dataRow.Table.Columns;

            int i = 0;
            // Set the parameters values
            foreach (IDataParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null ||
                    commandParameter.ParameterName.Length <= 1)
                    throw new InvalidOperationException(string.Format(
                        "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                        i, commandParameter.ParameterName));

                if (columns.Contains(commandParameter.ParameterName))
                    commandParameter.Value = dataRow[commandParameter.ParameterName];
                else if (columns.Contains(commandParameter.ParameterName.Substring(1)))
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];

                i++;
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of IDataParameters
        /// </summary>
        /// <param name="commandParameters">Array of IDataParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        /// <exception cref="System.ArgumentException">Thrown if an incorrect number of parameters are passed.</exception>
        protected void AssignParameterValues(IDataParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the IDataParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length, k = 0; i < j; i++)
            {
                if (commandParameters[i].Direction != ParameterDirection.ReturnValue)
                {
                    // If the current array value derives from IDataParameter, then assign its Value property
                    if (parameterValues[k] is IDataParameter)
                    {
                        IDataParameter paramInstance;
                        paramInstance = (IDataParameter)parameterValues[k];
                        if (paramInstance.Direction == ParameterDirection.ReturnValue)
                        {
                            paramInstance = (IDataParameter)parameterValues[++k];
                        }
                        if (paramInstance.Value == null)
                        {
                            commandParameters[i].Value = DBNull.Value;
                        }
                        else
                        {
                            commandParameters[i].Value = paramInstance.Value;
                        }
                    }
                    else if (parameterValues[k] == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = parameterValues[k];
                    }
                    k++;
                }
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>
        /// <param name="command">The IDbCommand to be prepared</param>
        /// <param name="connection">A valid IDbConnection, on which to execute this command</param>
        /// <param name="transaction">A valid IDbTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null.</exception>
        protected void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDataParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        /// <summary>
        /// This method clears (if necessary) the connection, transaction, command type and parameters 
        /// from the provided command
        /// </summary>
        /// <remarks>
        /// Not implemented here because the behavior of this method differs on each data provider. 
        /// </remarks>
        /// <param name="command">The IDbCommand to be cleared</param>
        protected void ClearCommand(IDbCommand command)
        {
            // do nothing by default
        }

        #endregion private utility methods

        #region GetParameter
        /// <summary>
        /// Get a SqlParameter for use in a SQL command
        /// </summary>
        /// <param name="name">The name of the parameter to create</param>
        /// <param name="value">The value of the specified parameter</param>
        /// <returns>A SqlParameter object</returns>
        public static SqlParameter GetParameter(string name, object value)
        {
            return (SqlParameter)(GetParameter(name, value));
        }

        /// <summary>
        /// Get a SqlParameter for use in a SQL command
        /// </summary>
        /// <param name="name">The name of the parameter to create</param>
        /// <param name="dbType">The System.Data.DbType of the parameter</param>
        /// <param name="size">The size of the parameter</param>
        /// <param name="direction">The System.Data.ParameterDirection of the parameter</param>
        /// <returns>A SqlParameter object</returns>
        public static SqlParameter GetParameter(string name, DbType dbType, int size, ParameterDirection direction)
        {
            return (SqlParameter)(GetParameter(name, dbType, size, direction));
        }

        /// <summary>
        /// Get a SqlParameter for use in a SQL command
        /// </summary>
        /// <param name="name">The name of the parameter to create</param>
        /// <param name="dbType">The System.Data.DbType of the parameter</param>
        /// <param name="size">The size of the parameter</param>
        /// <param name="sourceColumn">The source column of the parameter</param>
        /// <param name="sourceVersion">The System.Data.DataRowVersion of the parameter</param>
        /// <returns>A SqlParameter object</returns>
        public static SqlParameter GetParameter(string name, DbType dbType, int size, string sourceColumn, DataRowVersion sourceVersion)
        {
            return (SqlParameter)GetParameter(name, dbType, size, sourceColumn, sourceVersion);
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// Execute an IDbCommand (that returns no resultset) against the database
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public int ExecuteNonQuery(IDbCommand command)
        {
            bool mustCloseConnection = false;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
                mustCloseConnection = true;
            }

            if (command == null) throw new ArgumentNullException("command");

            int returnVal;

            returnVal = command.ExecuteNonQuery();

            if (mustCloseConnection)
            {
                command.Connection.Close();
            }

            return returnVal;
        }
        /// <summary>
        /// Execute an IDbCommand (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteNonQuery(commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public int ExecuteNonQuery(CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );

            // Create & open an IDbConnection, and dispose of it after we are done
            using (IDbConnection connection = GetConnection())
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// </remarks>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored prcedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            #region For storing sp name in trace

            storedProcedure = spName;

            #endregion


            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discoveryu
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteNonQuery(CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {
                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns no resultset and takes no parameters) against the provided IDbConnection. 
        /// </summary>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteNonQuery(connection, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns no resultset) against the specified IDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDbParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            IDbCommand cmd = connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (IDbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(cmd);

            // Finally, execute the command
            int retval = ExecuteNonQuery(cmd);

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output parameters
            // cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }

            #region Execution Status
            if (Convert.ToBoolean(retval))
            {
                //Utility.LogTrace(traceLog,"Execution Status : Sucess");
            }
            else
            {
                //Utility.LogTrace(traceLog,"Execution Status : Failure");
            }
            #endregion
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns no resultset) against the specified IDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// </remarks>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public int ExecuteNonQuery(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discoveryu
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns no resultset and takes no parameters) against the provided IDbTransaction. 
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteNonQuery(transaction, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns no resultset) against the specified IDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            IDbCommand cmd = transaction.Connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(cmd);

            // Finally, execute the command
            int retval = ExecuteNonQuery(cmd);

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output parameters
            // cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns no resultset) against the specified 
        /// IDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discoveryu
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDbParameters
                    return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataset

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <param name="command">The IDbCommand object to use</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public DataSet ExecuteDataset(IDbCommand command)
        {
            bool mustCloseConnection = false;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
                mustCloseConnection = true;
            }

            // Create the DataAdapter & DataSet
            IDbDataAdapter da = null;
            try
            {
                da = GetDataAdapter();
                da.SelectCommand = command;

                DataSet ds = new DataSet();

                try
                {

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    // Don't just throw ex.  It changes the call stack.  But we want the ex around for debugging, so...
                    Debug.WriteLine(ex);
                    throw;
                }

                // Detach the IDataParameters from the command object, so they can be used again
                // Don't do this...screws up output params -- cjb 
                //command.Parameters.Clear();

                // Return the DataSet
                return ds;
            }
            finally
            {
                if (mustCloseConnection)
                {
                    command.Connection.Close();
                }
                if (da != null)
                {
                    IDisposable id = da as IDisposable;
                    if (id != null)
                        id.Dispose();
                }
            }
        }
        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteDataset(commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new IDbParameter("@prodid", 24));
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDbParamters used to execute the command</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public DataSet ExecuteDataset(CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );

            // Create & open an IDbConnection, and dispose of it after we are done
            using (IDbConnection connection = GetConnection())
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(connString, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        public DataSet ExecuteDataset(string spName, params object[] parameterValues)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteDataset(CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {
                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteDataset(CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbConnection. 
        /// </summary>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteDataset(connection, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new IDataParameter("@prodid", 24));
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            IDbCommand cmd = connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (IDbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(cmd);

            DataSet ds = ExecuteDataset(cmd);

            if (mustCloseConnection)
            {
                connection.Close();
            }
            // Return the DataSet
            return ds;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(conn, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public DataSet ExecuteDataset(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteDataset(connection, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {
                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbTransaction. 
        /// </summary>
        /// <example><code>
        ///  DataSet ds = helper.ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteDataset(transaction, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new IDataParameter("@prodid", 24));
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            IDbCommand cmd = transaction.Connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(cmd);

            return ExecuteDataset(cmd);

        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified 
        /// IDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// DataSet ds = helper.ExecuteDataset(tran, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public DataSet ExecuteDataset(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteDataset

        #region ExecuteReader
        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <param name="command">The IDbCommand object to use</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public IDataReader ExecuteReader(IDbCommand command)
        {
            return ExecuteReader(command, AdoConnectionOwnership.External);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <param name="command">The IDbCommand object to use</param>
        /// <param name="connectionOwnership">Enum indicating whether the connection was created internally or externally.</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        protected IDataReader ExecuteReader(IDbCommand command, AdoConnectionOwnership connectionOwnership)
        {
            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
                connectionOwnership = AdoConnectionOwnership.Internal;
            }

            // Create a reader
            IDataReader dataReader;

            // Call ExecuteReader with the appropriate CommandBehavior
            if (connectionOwnership == AdoConnectionOwnership.External)
            {
                dataReader = command.ExecuteReader();
            }
            else
            {
                try
                {
                    dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    // Don't just throw ex.  It changes the call stack.  But we want the ex around for debugging, so...
                    Debug.WriteLine(ex);
                    throw;
                }
            }

            ClearCommand(command);

            return dataReader;
        }
        /// <summary>
        /// Create and prepare an IDbCommand, and call ExecuteReader with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        /// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        /// 
        /// If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="connection">A valid IDbConnection, on which to execute this command</param>
        /// <param name="transaction">A valid IDbTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="connectionOwnership">Indicates whether the connection parameter was provided by the caller, or created by AdoHelper</param>
        /// <returns>IDataReader containing the results of the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        private IDataReader ExecuteReader(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDataParameter[] commandParameters, AdoConnectionOwnership connectionOwnership)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Create a command and prepare it for execution
            IDbCommand cmd = connection.CreateCommand();
            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                CleanParameterSyntax(cmd);

                // override conenctionOwnership if we created the connection in PrepareCommand
                if (mustCloseConnection)
                {
                    connectionOwnership = AdoConnectionOwnership.Internal;
                }

                // Create a reader
                IDataReader dataReader;

                dataReader = ExecuteReader(cmd, connectionOwnership);

                ClearCommand(cmd);

                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw;
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteReader(commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public IDataReader ExecuteReader(CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();

                // Call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(connection, null, commandType, commandText, commandParameters, AdoConnectionOwnership.Internal);
            }
            catch
            {
                // If we fail to return the IDataReader, we need to close the connection ourselves
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }

        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// IDataReader dr = helper.ExecuteReader(connString, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public IDataReader ExecuteReader(string spName, params object[] parameterValues)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteReader(CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(spName, includeReturnValue);

                    AssignParameterValues(commandParameters, parameterValues);

                    return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbConnection. 
        /// </summary>
        /// <example>
        /// <code>
        /// IDataReader dr = helper.ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>an IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteReader(connection, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <example>
        /// <code>
        /// IDataReader dr = helper.ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new IDataParameter("@prodid", 24));
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>an IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            // Pass through the call to the private overload using a null transaction value and an externally owned connection
            return ExecuteReader(connection, (IDbTransaction)null, commandType, commandText, commandParameters, AdoConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// IDataReader dr = helper.ExecuteReader(conn, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public IDataReader ExecuteReader(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteReader(connection, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(connection, spName, includeReturnValue);

                    AssignParameterValues(commandParameters, parameterValues);

                    return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbTransaction. 
        /// </summary>
        /// <example><code>
        ///  IDataReader dr = helper.ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteReader(transaction, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <example>
        /// <code>
        /// IDataReader dr = helper.ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new IDataParameter("@prodid", 24));
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");

            // Pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, AdoConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified
        /// IDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// IDataReader dr = helper.ExecuteReader(tran, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public IDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteReader(transaction, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName, includeReturnValue);

                    AssignParameterValues(commandParameters, parameterValues);

                    return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteReader

        #region ExecuteScalar
        /// <summary>
        /// Execute an IDbCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public object ExecuteScalar(IDbCommand command)
        {
            bool mustCloseConnection = false;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
                mustCloseConnection = true;
            }

            // Execute the command & return the results
            object retval = command.ExecuteScalar();

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output params
            // command.Parameters.Clear();

            if (mustCloseConnection)
            {
                command.Connection.Close();
            }

            return retval;
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <example>
        /// <code>
        /// int orderCount = (int)helper.ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteScalar(commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public object ExecuteScalar(CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            // Create & open an IDbConnection, and dispose of it after we are done
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
            finally
            {
                IDisposable id = connection as IDisposable;
                if (id != null) id.Dispose();
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// int orderCount = (int)helper.ExecuteScalar(connString, "GetOrderCount", 24, 36);
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public object ExecuteScalar(string spName, params object[] parameterValues)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteScalar(CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a 1x1 resultset and takes no parameters) against the provided IDbConnection. 
        /// </summary>
        /// <example>
        /// <code>
        /// int orderCount = (int)helper.ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDbParameters
            return ExecuteScalar(connection, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a 1x1 resultset) against the specified IDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            IDbCommand cmd = connection.CreateCommand();

            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (IDbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(cmd);

            // Execute the command & return the results
            object retval = ExecuteScalar(cmd);

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output parameters
            // cmd.Parameters.Clear();

            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a 1x1 resultset) against the specified IDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// int orderCount = (int)helper.ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public object ExecuteScalar(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteScalar(connection, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {

                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a 1x1 resultset and takes no parameters) against the provided IDbTransaction. 
        /// </summary>
        /// <example>
        /// <code>
        /// int orderCount = (int)helper.ExecuteScalar(tran, CommandType.StoredProcedure, "GetOrderCount");
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteScalar(transaction, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a 1x1 resultset) against the specified IDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDbParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            IDbCommand cmd = transaction.Connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(cmd);

            // Execute the command & return the results
            object retval = ExecuteScalar(cmd);

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output parameters
            // cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a 1x1 resultset) against the specified
        /// IDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// int orderCount = (int)helper.ExecuteScalar(tran, "GetOrderCount", 24, 36);
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the transaction is rolled back or commmitted</exception>
        public object ExecuteScalar(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, iDataParameterValues);
                }
                else
                {
                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteScalar

        #region ExecuteXmlReader
        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbConnection. 
        /// </summary>
        /// <example>
        /// <code>
        /// XmlReader r = helper.ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(IDbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteXmlReader(connection, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <example>
        /// <code>
        /// XmlReader r = helper.ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", GetParameter("@prodid", 24));
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public XmlReader ExecuteXmlReader(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Create a command and prepare it for execution
            IDbCommand cmd = connection.CreateCommand();
            try
            {
                PrepareCommand(cmd, connection, (IDbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
                CleanParameterSyntax(cmd);

                return ExecuteXmlReader(cmd);
            }
            catch (Exception ex)
            {
                if (mustCloseConnection)
                    connection.Close();
                // Don't just throw ex.  It changes the call stack.  But we want the ex around for debugging, so...
                Debug.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// XmlReader r = helper.ExecuteXmlReader(conn, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="spName">The name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public XmlReader ExecuteXmlReader(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                ArrayList tempParameter = new ArrayList();
                foreach (IDataParameter parameter in GetSpParameterSet(connection, spName))
                {
                    tempParameter.Add(parameter);
                }
                IDataParameter[] commandParameters = (IDataParameter[])tempParameter.ToArray(typeof(IDataParameter));

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of IDataParameters
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbTransaction. 
        /// </summary>
        /// <example>
        /// <code>
        /// XmlReader r = helper.ExecuteXmlReader(tran, CommandType.StoredProcedure, "GetOrders");
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of IDataParameters
            return ExecuteXmlReader(transaction, commandType, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <example>
        /// <code>
        /// XmlReader r = helper.ExecuteXmlReader(tran, CommandType.StoredProcedure, "GetOrders", GetParameter("@prodid", 24));
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            IDbCommand cmd = transaction.Connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(cmd);

            // Create the DataAdapter & DataSet
            XmlReader retval = ExecuteXmlReader(cmd);

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output params
            // cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified 
        /// IDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// XmlReader r = helper.ExecuteXmlReader(trans, "GetOrders", 24, 36);
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="spName">The name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public XmlReader ExecuteXmlReader(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                ArrayList tempParameter = new ArrayList();
                foreach (IDataParameter parameter in GetSpParameterSet(transaction.Connection, spName))
                {
                    tempParameter.Add(parameter);
                }
                IDataParameter[] commandParameters = (IDataParameter[])tempParameter.ToArray(typeof(IDataParameter));

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of IDataParameters
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteXmlReader

        #region FillDataset
        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <param name="dataSet">A DataSet wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public void FillDataset(IDbCommand command, DataSet dataSet, string[] tableNames)
        {
            bool mustCloseConnection = false;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
                mustCloseConnection = true;
            }

            // Create the DataAdapter & DataSet
            IDbDataAdapter dataAdapter = null;
            try
            {
                dataAdapter = GetDataAdapter();
                dataAdapter.SelectCommand = command;

                // Add the table mappings specified by the user
                if (tableNames != null && tableNames.Length > 0)
                {
                    string tableName = "Table";
                    for (int index = 0; index < tableNames.Length; index++)
                    {
                        if (tableNames[index] == null || tableNames[index].Length == 0)
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        dataAdapter.TableMappings.Add(
                            tableName + (index == 0 ? "" : index.ToString()),
                            tableNames[index]);
                    }
                }

                // Fill the DataSet using default values for DataTable names, etc
                dataAdapter.Fill(dataSet);

                if (mustCloseConnection)
                {
                    command.Connection.Close();
                }

                // Detach the IDataParameters from the command object, so they can be used again
                // don't do this...screws up output params  --cjb
                // command.Parameters.Clear();
            }
            finally
            {
                IDisposable id = dataAdapter as IDisposable;
                if (id != null) id.Dispose();
            }

        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <example>
        /// <code>
        /// helper.FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="dataSet">A DataSet wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            // Create & open an IDbConnection, and dispose of it after we are done
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandType, commandText, dataSet, tableNames);
            }
            finally
            {
                IDisposable id = connection as IDisposable;
                if (id != null) id.Dispose();
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <param name="dataSet">A DataSet wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if any of the IDataParameters.ParameterNames are null, or if the parameter count does not match the number of values supplied</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public void FillDataset(CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params IDataParameter[] commandParameters)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            // Create & open an IDbConnection, and dispose of it after we are done
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
            }
            finally
            {
                IDisposable id = connection as IDisposable;
                if (id != null) id.Dispose();
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// helper.FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, 24);
        /// </code></example>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>    
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if the parameter count does not match the number of values supplied</exception>
        public void FillDataset(string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (dataSet == null) throw new ArgumentNullException("dataSet");


            // Create & open an IDbConnection, and dispose of it after we are done
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, spName, dataSet, tableNames, parameterValues);
            }
            finally
            {
                IDisposable id = connection as IDisposable;
                if (id != null) id.Dispose();
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbConnection. 
        /// </summary>
        /// <example>
        /// <code>
        /// helper.FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>    
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public void FillDataset(IDbConnection connection, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="dataSet">A DataSet wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public void FillDataset(IDbConnection connection, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params IDataParameter[] commandParameters)
        {
            FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// helper.FillDataset(conn, "GetOrders", ds, new string[] {"orders"}, 24, 36);
        /// </code></example>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public void FillDataset(IDbConnection connection, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, iDataParameterValues);
                }
                else
                {

                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset and takes no parameters) against the provided IDbTransaction. 
        /// </summary>
        /// <example>
        /// <code>
        /// helper.FillDataset(tran, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>    
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public void FillDataset(IDbTransaction transaction, CommandType commandType,
            string commandText,
            DataSet dataSet, string[] tableNames)
        {
            FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Execute an IDbCommand (that returns a resultset) against the specified IDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="dataSet">A DataSet wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public void FillDataset(IDbTransaction transaction, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params IDataParameter[] commandParameters)
        {
            FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified 
        /// IDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// helper.FillDataset(tran, "GetOrders", ds, new string[] {"orders"}, 24, 36);
        /// </code></example>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public void FillDataset(IDbTransaction transaction, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IDataParameter[] iDataParameterValues = GetDataParameters(parameterValues.Length);

                // if we've been passed IDataParameters, don't do parameter discovery
                if (AreParameterValuesIDataParameters(parameterValues, iDataParameterValues))
                {
                    FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, iDataParameterValues);
                }
                else
                {
                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    bool includeReturnValue = CheckForReturnValueParameter(parameterValues);
                    IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName, includeReturnValue);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of IDataParameters
                    FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
                }
            }
            else
            {
                // Otherwise we can just call the SP without params
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Private helper method that execute an IDbCommand (that returns a resultset) against the specified IDbTransaction and IDbConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="transaction">A valid IDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="dataSet">A DataSet wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of IDataParameters used to execute the command</param>
        private void FillDataset(IDbConnection connection, IDbTransaction transaction, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params IDataParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            // Create a command and prepare it for execution
            IDbCommand command = connection.CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            CleanParameterSyntax(command);

            FillDataset(command, dataSet, tableNames);

            if (mustCloseConnection)
            {
                connection.Close();
            }
        }
        #endregion

        #region UpdateDataset
        /// <summary>
        /// This method consumes the RowUpdatingEvent and passes it on to the consumer specifed in the call to UpdateDataset
        /// </summary>
        /// <param name="obj">The object that generated the event</param>
        /// <param name="e">The System.Data.Common.RowUpdatingEventArgs</param>
        protected void RowUpdating(object obj, System.Data.Common.RowUpdatingEventArgs e)
        {
            if (this.m_rowUpdating != null)
                m_rowUpdating(obj, e);
        }

        /// <summary>
        /// This method consumes the RowUpdatedEvent and passes it on to the consumer specifed in the call to UpdateDataset
        /// </summary>
        /// <param name="obj">The object that generated the event</param>
        /// <param name="e">The System.Data.Common.RowUpdatingEventArgs</param>
        protected void RowUpdated(object obj, System.Data.Common.RowUpdatedEventArgs e)
        {
            if (this.m_rowUpdated != null)
                m_rowUpdated(obj, e);
        }

        /// <summary>
        /// Set up a command for updating a DataSet.
        /// </summary>
        /// <param name="command">command object to prepare</param>
        /// <param name="mustCloseConnection">output parameter specifying whether the connection used should be closed by the DAAB</param>
        /// <returns>An IDbCommand object</returns>
        protected IDbCommand SetCommand(IDbCommand command, out bool mustCloseConnection)
        {
            mustCloseConnection = false;
            if (command != null)
            {
                IDataParameter[] commandParameters = new IDataParameter[command.Parameters.Count];
                command.Parameters.CopyTo(commandParameters, 0);
                command.Parameters.Clear();
                this.PrepareCommand(command, command.Connection, null, command.CommandType, command.CommandText, commandParameters, out mustCloseConnection);
                CleanParameterSyntax(command);
            }

            return command;
        }

        /// <summary>
        /// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
        /// </summary>
        /// <example>
        /// <code>
        /// helper.UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        /// </code></example>
        /// <param name="insertCommand">A valid SQL statement or stored procedure to insert new records into the data source</param>
        /// <param name="deleteCommand">A valid SQL statement or stored procedure to delete records from the data source</param>
        /// <param name="updateCommand">A valid SQL statement or stored procedure used to update records in the data source</param>
        /// <param name="dataSet">The DataSet used to update the data source</param>
        /// <param name="tableName">The DataTable used to update the data source.</param>
        public void UpdateDataset(IDbCommand insertCommand, IDbCommand deleteCommand, IDbCommand updateCommand, DataSet dataSet, string tableName)
        {
            UpdateDataset(insertCommand, deleteCommand, updateCommand, dataSet, tableName, null, null);
        }

        /// <summary> 
        /// Executes the IDbCommand for each inserted, updated, or deleted row in the DataSet also implementing RowUpdating and RowUpdated Event Handlers 
        /// </summary> 
        /// <example> 
        /// <code>
        /// RowUpdatingEventHandler rowUpdatingHandler = new RowUpdatingEventHandler( OnRowUpdating ); 
        /// RowUpdatedEventHandler rowUpdatedHandler = new RowUpdatedEventHandler( OnRowUpdated ); 
        /// helper.UpdateDataSet(sqlInsertCommand, sqlDeleteCommand, sqlUpdateCommand, dataSet, "Order", rowUpdatingHandler, rowUpdatedHandler); 
        /// </code></example> 
        /// <param name="insertCommand">A valid SQL statement or stored procedure to insert new records into the data source</param> 
        /// <param name="deleteCommand">A valid SQL statement or stored procedure to delete records from the data source</param> 
        /// <param name="updateCommand">A valid SQL statement or stored procedure used to update records in the data source</param> 
        /// <param name="dataSet">The DataSet used to update the data source</param> 
        /// <param name="tableName">The DataTable used to update the data source.</param> 
        /// <param name="rowUpdatingHandler">RowUpdatingEventHandler</param> 
        /// <param name="rowUpdatedHandler">RowUpdatedEventHandler</param> 
        public void UpdateDataset(IDbCommand insertCommand, IDbCommand deleteCommand, IDbCommand updateCommand,
            DataSet dataSet, string tableName, RowUpdatingHandler rowUpdatingHandler, RowUpdatedHandler rowUpdatedHandler)
        {
            int rowsAffected = 0;

            if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

            // Create an IDbDataAdapter, and dispose of it after we are done
            IDbDataAdapter dataAdapter = null;
            try
            {
                bool mustCloseUpdateConnection = false;
                bool mustCloseInsertConnection = false;
                bool mustCloseDeleteConnection = false;

                dataAdapter = GetDataAdapter();

                // Set the data adapter commands
                dataAdapter.UpdateCommand = SetCommand(updateCommand, out mustCloseUpdateConnection);
                dataAdapter.InsertCommand = SetCommand(insertCommand, out mustCloseInsertConnection);
                dataAdapter.DeleteCommand = SetCommand(deleteCommand, out mustCloseDeleteConnection);

                AddUpdateEventHandlers(dataAdapter, rowUpdatingHandler, rowUpdatedHandler);

                if (dataAdapter is DbDataAdapter)
                {
                    // Update the DataSet changes in the data source
                    try
                    {
                        rowsAffected = ((DbDataAdapter)dataAdapter).Update(dataSet, tableName);
                    }
                    catch (Exception ex)
                    {
                        // Don't just throw ex.  It changes the call stack.  But we want the ex around for debugging, so...
                        Debug.WriteLine(ex);
                        throw;
                    }
                }
                else
                {
                    dataAdapter.TableMappings.Add(tableName, "Table");

                    // Update the DataSet changes in the data source
                    rowsAffected = dataAdapter.Update(dataSet);
                }

                // Commit all the changes made to the DataSet
                dataSet.Tables[tableName].AcceptChanges();

                if (mustCloseUpdateConnection)
                {
                    updateCommand.Connection.Close();
                }
                if (mustCloseInsertConnection)
                {
                    insertCommand.Connection.Close();
                }
                if (mustCloseDeleteConnection)
                {
                    deleteCommand.Connection.Close();
                }
            }
            finally
            {
                IDisposable id = dataAdapter as IDisposable;
                if (id != null) id.Dispose();
            }
        }
        #endregion

        #region CreateCommand
        /// <summary>
        /// Simplify the creation of a Sql command object by allowing
        /// a stored procedure and optional parameters to be provided
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(connenctionString, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
        /// <returns>A valid SqlCommand object</returns>
        public static SqlCommand CreateCommand(string connectionString, string spName, params string[] sourceColumns)
        {
            return CreateCommand(connectionString, spName, sourceColumns) as SqlCommand;
        }
        /// <summary>
        /// Simplify the creation of a Sql command object by allowing
        /// a stored procedure and optional parameters to be provided
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
        /// <returns>A valid SqlCommand object</returns>
        public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            return CreateCommand(connection, spName, sourceColumns) as SqlCommand;
        }
        /// <summary>
        /// Simplify the creation of a Sql command object by allowing
        /// a stored procedure and optional parameters to be provided
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(connenctionString, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">A valid SQL string to execute</param>
        /// <param name="commandType">The CommandType to execute (i.e. StoredProcedure, Text)</param>
        /// <param name="commandParameters">The SqlParameters to pass to the command</param>
        /// <returns>A valid SqlCommand object</returns>
        public static SqlCommand CreateCommand(string connectionString, string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            return CreateCommand(connectionString, commandText, commandType, commandParameters) as SqlCommand;
        }
        /// <summary>
        /// Simplify the creation of a Sql command object by allowing
        /// a stored procedure and optional parameters to be provided
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="commandText">A valid SQL string to execute</param>
        /// <param name="commandType">The CommandType to execute (i.e. StoredProcedure, Text)</param>
        /// <param name="commandParameters">The SqlParameters to pass to the command</param>
        /// <returns>A valid SqlCommand object</returns>
        public static SqlCommand CreateCommand(SqlConnection connection, string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            return CreateCommand(connection, commandText, commandType, commandParameters) as SqlCommand;
        }
        #endregion

        #region ExecuteNonQueryTypedParams
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns no resultset) 
        /// against the database specified in the connection string using the 
        /// dataRow column values as the stored procedure's parameters values.
        /// This method will assign the parameter values based on row values.
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public int ExecuteNonQueryTypedParams(IDbCommand command, DataRow dataRow)
        {
            int retVal = 0;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Set the parameters values
                AssignParameterValues(command.Parameters, dataRow);

                retVal = ExecuteNonQuery(command);
            }
            else
            {
                retVal = ExecuteNonQuery(command);
            }

            return retVal;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns no resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        public int ExecuteNonQueryTypedParams(String spName, DataRow dataRow)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteNonQuery(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns no resultset) against the specified IDbConnection 
        /// using the dataRow column values as the stored procedure's parameters values.  
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public int ExecuteNonQueryTypedParams(IDbConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns no resultset) against the specified
        /// IDbTransaction using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public int ExecuteNonQueryTypedParams(IDbTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // Sf the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region ExecuteDatasetTypedParams
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will assign the paraemter values based on row values.
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public DataSet ExecuteDatasetTypedParams(IDbCommand command, DataRow dataRow)
        {
            DataSet ds = null;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Set the parameters values
                AssignParameterValues(command.Parameters, dataRow);


                ds = ExecuteDataset(command);
            }
            else
            {
                ds = ExecuteDataset(command);
            }

            return ds;
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        public DataSet ExecuteDatasetTypedParams(String spName, DataRow dataRow)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            //If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteDataset(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteDataset(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the dataRow column values as the store procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public DataSet ExecuteDatasetTypedParams(IDbConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbTransaction 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A DataSet containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public DataSet ExecuteDatasetTypedParams(IDbTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion

        #region ExecuteReaderTypedParams
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will assign the parameter values based on parameter order.
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public IDataReader ExecuteReaderTypedParams(IDbCommand command, DataRow dataRow)
        {
            IDataReader reader = null;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Set the parameters values
                AssignParameterValues(command.Parameters, dataRow);

                reader = ExecuteReader(command);
            }
            else
            {
                reader = ExecuteReader(command);
            }

            return reader;
        }
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        public IDataReader ExecuteReaderTypedParams(String spName, DataRow dataRow)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteReader(CommandType.StoredProcedure, spName);
            }
        }


        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public IDataReader ExecuteReaderTypedParams(IDbConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbTransaction 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>A IDataReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public IDataReader ExecuteReaderTypedParams(IDbTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region ExecuteScalarTypedParams
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will assign the parameter values based on parameter order.
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public object ExecuteScalarTypedParams(IDbCommand command, DataRow dataRow)
        {
            object retVal = null;

            // Clean Up Parameter Syntax
            CleanParameterSyntax(command);

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Set the parameters values
                AssignParameterValues(command.Parameters, dataRow);

                retVal = ExecuteScalar(command);
            }
            else
            {
                retVal = ExecuteScalar(command);
            }

            return retVal;
        }
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        public object ExecuteScalarTypedParams(String spName, DataRow dataRow)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteScalar(CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a 1x1 resultset) against the specified IDbConnection 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public object ExecuteScalarTypedParams(IDbConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a 1x1 resultset) against the specified IDbTransaction
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public object ExecuteScalarTypedParams(IDbTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IDataParameter[] commandParameters = GetSpParameterSet(transaction.Connection, spName);

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region ExecuteXmlReaderTypedParams
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will assign the parameter values based on parameter order.
        /// </summary>
        /// <param name="command">The IDbCommand to execute</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        public XmlReader ExecuteXmlReaderTypedParams(IDbCommand command, DataRow dataRow)
        {
            if (command == null) throw new ArgumentNullException("command");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Set the parameters values
                AssignParameterValues(command.Parameters, dataRow);

                return ExecuteXmlReader(command);
            }
            else
            {
                return ExecuteXmlReader(command);
            }
        }
        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbConnection 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public XmlReader ExecuteXmlReaderTypedParams(IDbConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                ArrayList tempParameter = new ArrayList();
                foreach (IDataParameter parameter in GetSpParameterSet(connection, spName))
                {
                    tempParameter.Add(parameter);
                }
                IDataParameter[] commandParameters = (IDataParameter[])tempParameter.ToArray(typeof(IDataParameter));

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an IDbCommand (that returns a resultset) against the specified IDbTransaction 
        /// using the dataRow column values as the stored procedure's parameters values.
        /// This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="transaction">A valid IDbTransaction object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if transaction.Connection is null</exception>
        public XmlReader ExecuteXmlReaderTypedParams(IDbTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // If the row has values, the store procedure parameters must be initialized
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                ArrayList tempParameter = new ArrayList();
                foreach (IDataParameter parameter in GetSpParameterSet(transaction.Connection, spName))
                {
                    tempParameter.Add(parameter);
                }
                IDataParameter[] commandParameters = (IDataParameter[])tempParameter.ToArray(typeof(IDataParameter));

                // Set the parameters values
                AssignParameterValues(commandParameters, dataRow);

                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region Parameter Discovery Functions

        /// <summary>
        /// Checks for the existence of a return value parameter in the parametervalues
        /// </summary>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>true if the parameterValues contains a return value parameter, false otherwise</returns>
        private bool CheckForReturnValueParameter(object[] parameterValues)
        {
            bool hasReturnValue = false;
            foreach (object paramObject in parameterValues)
            {
                if (paramObject is IDataParameter)
                {
                    IDataParameter paramInstance = (IDataParameter)paramObject;
                    if (paramInstance.Direction == ParameterDirection.ReturnValue)
                    {
                        hasReturnValue = true;
                        break;
                    }
                }
            }
            return hasReturnValue;
        }

        /// <summary>
        /// Check to see if the parameter values passed to the helper are, in fact, IDataParameters.
        /// </summary>
        /// <param name="parameterValues">Array of parameter values passed to helper</param>
        /// <param name="iDataParameterValues">new array of IDataParameters built from parameter values</param>
        /// <returns>True if the parameter values are IDataParameters</returns>
        private bool AreParameterValuesIDataParameters(object[] parameterValues, IDataParameter[] iDataParameterValues)
        {
            bool areIDataParameters = true;

            for (int i = 0; i < parameterValues.Length; i++)
            {
                if (!(parameterValues[i] is IDataParameter))
                {
                    areIDataParameters = false;
                    break;
                }
                iDataParameterValues[i] = (IDataParameter)parameterValues[i];
            }
            return areIDataParameters;
        }


        /// <summary>
        /// Retrieves the set of IDataParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <returns>An array of IDataParameterParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        public IDataParameter[] GetSpParameterSet(string spName)
        {
            return GetSpParameterSet(spName, false);
        }

        /// <summary>
        /// Retrieves the set of IDataParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of IDataParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        public IDataParameter[] GetSpParameterSet(string spName, bool includeReturnValueParameter)
        {
            //if( connectionString == null || connectionString.Length == 0 ) throw new ArgumentNullException( "connectionString" );
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            using (IDbConnection connection = GetConnection())
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of IDataParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">A valid IDataConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <returns>An array of IDataParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public IDataParameter[] GetSpParameterSet(IDbConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>
        /// Retrieves the set of IDataParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of IDataParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public IDataParameter[] GetSpParameterSet(IDbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (connection as ICloneable == null) throw new ArgumentException("can´t discover parameters if the connection doesn´t implement the ICloneable interface", "connection");

            IDbConnection clonedConnection = (IDbConnection)((ICloneable)connection).Clone();
            return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
        }

        /// <summary>
        /// Retrieves the set of IDataParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of IDataParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        private IDataParameter[] GetSpParameterSetInternal(IDbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter":"");

            IDataParameter[] cachedParameters;

            cachedParameters = GetCachedParameterSet(connection,
                spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : ""));

            if (cachedParameters == null)
            {
                IDataParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                CacheParameterSet(connection,
                    spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : ""), spParameters);

                cachedParameters = ADOHelperParameterCache.CloneParameters(spParameters);
            }

            return cachedParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An array of IDataParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        public IDataParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            using (IDbConnection connection = GetConnection())
            {
                return GetCachedParameterSetInternal(connection, commandText);
            }
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An array of IDataParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        public IDataParameter[] GetCachedParameterSet(IDbConnection connection, string commandText)
        {
            return GetCachedParameterSetInternal(connection, commandText);
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An array of IDataParameters</returns>
        private IDataParameter[] GetCachedParameterSetInternal(IDbConnection connection, string commandText)
        {
            bool mustCloseConnection = false;
            // this way we control the connection, and therefore the connection string that gets saved as a hash key
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                mustCloseConnection = true;
            }

            IDataParameter[] parameters = ADOHelperParameterCache.GetCachedParameterSet(connection.ConnectionString, commandText);

            if (mustCloseConnection)
            {
                connection.Close();
            }

            return parameters;
        }

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters to be cached</param>
        public void CacheParameterSet(string connectionString, string commandText, params IDataParameter[] commandParameters)
        {
            using (IDbConnection connection = GetConnection())
            {
                CacheParameterSetInternal(connection, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters to be cached</param>
        public void CacheParameterSet(IDbConnection connection, string commandText, params IDataParameter[] commandParameters)
        {
            if (connection is ICloneable)
            {
                using (IDbConnection clonedConnection = (IDbConnection)((ICloneable)connection).Clone())
                {
                    CacheParameterSetInternal(clonedConnection, commandText, commandParameters);
                }
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connection">A valid IDbConnection</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters to be cached</param>
        private void CacheParameterSetInternal(IDbConnection connection, string commandText, params IDataParameter[] commandParameters)
        {
            // this way we control the connection, and therefore the connection string that gets saved as a hask key
            connection.Open();
            ADOHelperParameterCache.CacheParameterSet(connection.ConnectionString, commandText, commandParameters);
            connection.Close();
        }

        /// <summary>
        /// Resolve at run time the appropriate set of IDataParameters for a stored procedure
        /// </summary>
        /// <param name="connection">A valid IDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
        /// <returns>The parameter array discovered.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if spName is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connection is null</exception>
        private IDataParameter[] DiscoverSpParameterSet(IDbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                // not all providers have return value parameters...don't just remove this parameter indiscriminately
                if (cmd.Parameters.Count > 0 && ((IDataParameter)cmd.Parameters[0]).Direction == ParameterDirection.ReturnValue)
                {
                    cmd.Parameters.RemoveAt(0);
                }
            }

            IDataParameter[] discoveredParameters = new IDataParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (IDataParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        #endregion Parameter Discovery Functions

    }

    /// <summary>
    /// SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
    /// ability to discover parameters for stored procedures at run-time.
    /// </summary>
    public sealed class ADOHelperParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Deep copy of cached IDataParameter array
        /// </summary>
        /// <param name="originalParameters"></param>
        /// <returns></returns>
        internal static IDataParameter[] CloneParameters(IDataParameter[] originalParameters)
        {
            IDataParameter[] clonedParameters = new IDataParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (IDataParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #region caching functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters to be cached</param>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        internal static void CacheParameterSet(string connectionString, string commandText, params IDataParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for an IDbConnection</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <returns>An array of IDataParameters</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if commandText is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if connectionString is null</exception>
        internal static IDataParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            IDataParameter[] cachedParameters = paramCache[hashKey] as IDataParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion caching functions
    }
}