using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace AnalyticBrainsDO
{
    public class ResultDO
    {

        #region Declaration
        private IDataReader resultReader;
        private ArrayList resarraylist;
        private DataSet dtset;
        private int updateresult;
        private string strresult;
        private bool blresult;
        private DataSet edtset;
    
        #endregion

        #region Properties
        public int UpdateResult
        {
            get
            {
                return updateresult;
            }
            set
            {
                updateresult = value;
            }


        }
        public bool BoolResult
        {
            get
            {
                return blresult;
            }
            set
            {
                blresult = value;
            }


        }
        public string StrResult
        {
            get
            {
                return strresult;
            }
            set
            {
                strresult = value;
            }


        }
        public IDataReader ResultReader
        {
            get
            {
                return resultReader;
            }
            set
            {
                resultReader = value;
            }
        }
        public ArrayList ResultArrayList
        {
            get
            {
                return resarraylist;
            }
            set
            {
                resarraylist = value;
            }
        }
        public DataSet Resultdtset
        {
            get
            {
                return dtset;
            }
            set
            {
                dtset = value;
            }
        }
        public DataSet Emptydtset
        {
            get
            {
                return edtset;
            }
            set
            {
                edtset = value;
            }
        }
        #endregion
    }
}
