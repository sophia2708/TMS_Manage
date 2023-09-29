<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="PoliciesandProcedures.aspx.cs" Inherits="Includes_WebForm_PoliciesandProcedures"
    Title="Policies and Procedures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-New/js/jquery.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="../bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../DataTables-1.10.10/media/js/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <link href="../DataTables-1.10.10/media/css/dataTables.jqueryui.css" rel="stylesheet"
        type="text/css" />
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <link href="../bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="../DataTables-1.10.10/media/css/dataTables.jqueryui.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../DataTables-1.10.10/media/css/jquery.dataTables.css" rel="stylesheet"
        type="text/css" />
    <link href="../DataTables-1.10.11/media/css/dataTables.bootstrap4.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../DataTables-1.10.11/media/css/responsive.bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../DataTables-1.10.10/media/js/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/dataTables.responsive.min.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/responsive.bootstrap.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/bootstrap.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
            text-indent: 0;
        }
        h1
        {
            color: #365F91;
            font-family: Cambria, serif;
            font-style: normal;
            font-weight: bold;
            text-decoration: none;
            font-size: 31px;
            width: 100%;
            margin-bottom:3px !important;
        }
        a:hover
        {
            text-decoration: none;
            cursor: pointer;
        }
        li p{text-indent: 27px !important;}
        <%-- h2 + p{text-indent: 27px !important;}--%>
        
        .p, p
        {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 18px;
            margin: 0pt;
            padding-top:3px !important;
        }
        h3
        {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: bold;
            text-decoration: none;
            font-size: 14pt;
            margin:0 !important;
        }
        h2
        {
            color: #4F81BB;
            padding-left: 44pt !important;
            font-family: Cambria, serif;
            font-style: normal;
            font-weight: bold;
            text-decoration: none;
            font-size: 18pt;
            margin-bottom:3px !important;
        }
        .s1
        {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 7pt;
            vertical-align: 3pt;
        }
        .s2
        {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: bold;
            text-decoration: none;
            font-size: 11pt;
        }
        .s3
        {
            color: black;
            font-family: Calibri, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        .s4
        {
            color: black;
            font-family: Verdana, sans-serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 10pt;
        }
        li
        {
            display: block;
        }
        #l1
        {
            padding-left: 0pt;
            counter-reset: c1 1;
        }
        #l1 > li > *:first-child:before
        {
            counter-increment: c1;
            content: counter(c1, decimal) " ";
            color: #365F91;
            font-family: Cambria, serif;
            font-style: normal;
            font-weight: bold;
            text-decoration: none;
            font-size: 14pt;
        }
        #l1 > li:first-child > *:first-child:before
        {
            counter-increment: c1 0;
        }
        #l2
        {
            padding-left: 0pt;
        }
        #l2 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l3
        {
            padding-left: 0pt;
        }
        #l3 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l4
        {
            padding-left: 0pt;
        }
        #l4 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l5
        {
            padding-left: 0pt;
        }
        #l5 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l6
        {
            padding-left: 0pt;
        }
        #l6 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l7
        {
            padding-left: 0pt;
        }
        #l7 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l8
        {
            padding-left: 0pt;
        }
        #l8 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l9
        {
            padding-left: 0pt;
        }
        #l9 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        #l10
        {
            padding-left: 0pt;
        }
        #l10 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        li
        {
            display: block;
        }
        #l11
        {
            padding-left: 0pt;
        }
        #l11 > li > *:first-child:before
        {
            content: " ";
            color: black;
            font-family: Symbol, serif;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            font-size: 11pt;
        }
        table, tbody
        {
            vertical-align: top;
            overflow: visible;
        }
       
    </style>
    <p style="text-indent: 0pt; text-align: left;">
        <br />
    </p>  
    <ol id="">
        <li data-list-text="1">
            <h1 style="padding-top: 4pt; padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark0">1 Welcome Aboard</a></h1>
            <p style="padding-left: 27pt; text-indent: 0pt; line-height: 112%; text-align: left;">
                We, at Analytic Brains are happy to welcome you aboard. We, assure you that you
                will find the time you spend working with us, both personally rewarding and productive
                for the organization.</p>
        </li>
        <li data-list-text="">
            <h1 style="padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark1">2 Introduction</a></h1>
            <p style=" padding-left: 23pt; text-indent: 0pt; line-height: 115%;
                text-align: justify;">
                This employee handbook is a summary of policies, procedures and practices related
                to human resource and infrastructure management at <b>Analytic Brains Technologies Pvt.
                    Ltd</b>.</p>
            <p style="padding-left: 23pt; text-indent: 0pt; line-height: 113%; text-align: justify;">
                The Managing Director Mr. <b>Sundaresan Subramanian </b>leads the staff team in
                the development and implementation of the policies outlined in this manual. He will
                be assisted by Managers who should reference this manual to ensure organizational
                consistency in the application of these practices.</p>
            <p style="padding-left: 23pt; text-indent: 0pt; line-height: 113%; text-align: justify;">
                The <b>Admin </b>is responsible for the maintaining the procedures and systems which
                support human resource and infrastructure management for the organization. The employee
                designated as <b>Admin </b>will be available to answer questions, or provide clarification
                to any employee, on any content of this manual.</p>
            <p style="padding-left: 23pt; text-indent: 0pt; line-height: 115%; text-align: justify;">
                The organization’s benefits package is spelt out in the offer letter issued to each
                employee, on appointment. Questions regarding the offer letter may be directed to
                the said Managing Director.</p>
        </li>
        <li data-list-text="">
            <h1 style="padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark2">3 About the Organization</a></h1>
            <p style="padding-left: 23pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
                <b>Analytic Brains Technologies Pvt. Ltd</b> was established in 2010, by <b>Sundaresan
                    Subramanyan</b>, a financial technologist with two decades of experience in
                providing solutions to international clients in the Financial Services industry.
                The company is, today is a boutique organization, serving several local and international
                clients, providing them the following services:
            </p>
            <ul id="l2">
                <li data-list-text="">
                    <p style="padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Application Development</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Database Services</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 1pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Data Migration</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Business Intelligence</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Testing Services</p>
                </li>
            </ul>
        </li>
        <li data-list-text="">
            <h1 style="padding-top: 2pt; padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark3">4 Statement of Philosophy</a></h1>
            
            <p style=" padding-left: 23pt; text-indent: 0pt; line-height: 114%;
                text-align: justify;">
                Analytic Brains wishes to maintain a work environment that fosters personal and
                professional growth for all employees. Maintaining such an environment is the responsibility
                of every employee. Because of their role, managers and supervisors have the additional
                responsibility to lead in a manner which fosters such an environment.</p>
            <p style="padding-left: 23pt; text-indent: 0pt; text-align: justify;">
                It is the responsibility of all employees and managers to:</p>
            <ul id="l3">
                <li data-list-text="">
                    <p style="padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Foster cooperation and communication among each other</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Treat each other in a fair manner, with dignity and respect</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Promote harmony and teamwork in all relationships</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 59pt; text-indent: -18pt; line-height: 113%;
                        text-align: justify;">
                        Strive for mutual understanding of standards for performance expectations, and communicate
                        routinely to reinforce that understanding</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt;padding-left: 59pt; text-indent: -18pt; line-height: 112%; text-align: justify;">
                        Encourage and consider opinions of other employees or members, and invite their
                        participation in decisions that affect their work and their careers</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt;padding-left: 59pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        Seek to avoid workplace conflict, and if it occurs, respond fairly and quickly to
                        provide the means to resolve it</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt;padding-left: 59pt; text-indent: -18pt; line-height: 114%; text-align: justify;">
                        Administer all policies equitably and fairly, recognizing that jobs are different
                        but each is important; that individual performance should be recognized and measured
                        against predetermined standards; and that each employee has the right to fair treatment</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt;padding-left: 59pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        Recognize that employees in their personal lives may experience crisis and show
                        compassion and understanding if and when the same occurs</p>
                    <p style="text-indent: 0pt; text-align: left;">
                    </p>
                </li>
            </ul>
        </li>
        <li data-list-text="">
            <h1 style="padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark4">5 HR Processes</a></h1>
            <p style=" padding-left: 23pt; text-indent: 0pt; line-height: 113%;
                text-align: justify;">
                Though a small organization, we scrupulously follow HR processes. On joining the
                organization, to make you productive, from day-1, you will be provided with the
                following.</p>
            <h2 style="padding-left: 50pt; text-indent: 0pt; text-align: left;">
                <a name="bookmark5">Documents – Company to Employee</a></h2>
            <ul id="l4">
                <li data-list-text="">
                    <p style=" padding-left: 50pt; text-indent: -18pt; line-height: 115%;
                        text-align: left;">
                        When an employee is selected for appointment, (s)he will be issued offer letter
                        and appointment letter</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: left;">
                        On completion of verification, all original certificates and letters will be returned
                        to the employee, with the company retaining the photocopies</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: left;">
                        Upon joining the organization, the employee will be informed of the path where this
                        document (Employee Handbook) is available for reference, in the shared location
                        and a soft copy of this document will be emailed to the employee.</p>
                    <h2 style="padding-top: 2pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        <a name="bookmark5">Documents – Employee to Company</a></h2>
                </li>
                <li data-list-text="">
                    <p style=" padding-left: 50pt; text-indent: -18pt; text-align: left;">
                        Copy of offer letter, signed and acknowledged by the employee</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        Submit originals and photocopies of all certificates pertaining to educational qualifications
                        (starting from standard 10<span class="s1">th</span> to the highest qualification
                        obtained, including any certifications) for doing back ground check</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: left;">
                        Submit experience certificates evidencing work experience and pay slips, if any,
                        from previous organizations (original and photocopies)</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; text-align: left;">
                        Relieving letter issued by previous organizations (original and photocopies)</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        Provide information about bank account, to which salary to be credited, PAN and
                        Aadhaar Card details, with photocopies</p>
                </li>
                <li data-list-text="">

                    <p style="padding-left: 50pt; text-indent: -18pt; text-align: left;">
                        Passport size photo of the employee</p>
                    <h2 style="padding-top: 12pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        <a name="bookmark6">Induction</a></h2>
                </li>
                <li data-list-text="">
                    <p style=" padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        On reporting duty, the employee will be given a formal induction into the project
                        (whether internal or external), by the Reporting Manager</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; text-align: left;">
                        On completion of the induction, the employee will be assigned duties to be performed</p>
                    <h2 style=" padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        <a name="bookmark7">Employee Designation</a></h2>
                    <p style=" padding-left: 41pt; text-indent: 0pt; text-align: justify;">
                        Employees joining Analytic Brains maybe classified as:</p>
                    <ul id="l5">
                        <li data-list-text="">
                            <p style="padding-left: 100px; text-indent: -18pt; text-align: left;">
                                Programmer Trainee</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 2pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Programmer</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 2pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Programmer Analyst</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 1pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Senior Programmer</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 2pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Software Engineer</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 2pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Senior Software Engineer</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 2pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Analyst</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 1pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Senior Analyst</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 2pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Team Leader</p>
                        </li>
                        <li data-list-text="">
                            <p style="padding-top: 2pt; padding-left: 100px;; text-indent: -18pt; text-align: left;">
                                Project Leader</p>
                            <p style="padding-top: 11pt; padding-left: 41pt; text-indent: 0pt; line-height: 115%;
                                text-align: justify;">
                                A fresher joining as programmer trainee maybe on probation for a period of 6 months,
                                at the end of which, (s)he may be confirmed in service, subject to satisfactory
                                performance. Based on the designation, duties would be assigned to the employees.</p>
                            <h2 style="padding-top: 4pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                                <a name="bookmark8">Employee Duties</a></h2>
                            <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 113%;
                                text-align: justify;">
                                Attached to the Offer of Employment, is a broad description of the job and the associated
                                responsibilities, along with any additional tasks that may possibly be required.
                                This document will be used to evaluate performance of the employee, both during
                                the probation period and after. Employees are advised to seek clarification, if
                                they are unclear of the contents.</p>
                            <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 114%;
                                text-align: justify;">
                                From time to time, it may be necessary to amend an employee’s job description. These
                                amendments will be discussed with the employee in advance however; the final decision
                                on implementation will be made by management.</p>
                            <h2 style="padding-left: 50pt; text-indent: 0pt; text-align: left;">
                                <a name="bookmark9">Performance Appraisals</a></h2>
                            <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 114%;
                                text-align: justify;">
                                Performance reviews, for all employees, will occur near the end of March, and annually
                                thereafter. However, every employee will be provided regular feedback of their performance,
                                by the manager concerned. The purpose of the annual performance review meeting is
                                to review successes and challenges from the preceding year, and to establish the
                                objectives for the coming year. This would also be the opportunity for either party
                                to identify and recommend professional development opportunities which may assist
                                the employee in their day to day work and to grow within the organization. Once
                                complete, both parties shall sign off on the final document and it shall be added
                                to the employee’s personnel file.</p>
                            <h2 style="padding-top: 8pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                                <a name="bookmark10">Payroll and Salary Revision</a></h2>
                        </li>
                    </ul>
                </li>
                <li data-list-text="">
                    <p style=" padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: justify;">
                        The employees will be paid salary, in accordance with the offer letter provided
                        on recruitment</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        The time sheet entered by the employees will be scrutinised and consolidated by
                        Admin and salary credited to the employees’ bank accounts, on the last working day
                        of every month. Payslip will be sent to the employee’s business email account</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 117%; text-align: justify;">
                        Revision of salary and bonus payment (if any) will occur every year in the month
                        of April, following performance appraisal</p>
                    <h2 style="padding-top: 5pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        <a name="bookmark11">Hours of Work</a></h2>
                    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 113%;
                        text-align: justify;">
                        The regular office hours for the organization are 9.30 a.m. to 6:30 p.m., Monday
                        through Friday inclusive (excluding holidays). However, on days when there are meetings
                        with the client, or where deliverables are to be made, employees may be required
                        to come early or leave late, on completion of work. All employees are expected to
                        work a minimum of 8 hours per day, <b>excluding </b>one hour of lunch and tea break
                        in between.</p>
                    <p style="padding-left: 41pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
                        Employees are required to notify their supervisor, in advance, of planned leave.
                        Unplanned absences from the office should be reported to the employee’s supervisor
                        as soon as could reasonably be expected. At the discretion of the Managing Director,
                        depending on circumstances, employees may be allowed to work from home for specific
                        periods of time.</p>
                    <p style="padding-left: 41pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
                        In case of unforeseen circumstances (such as heavy rains or other causes), the office
                        cannot be opened, employees may be required to compensate for the same, by attending
                        office on weekly holidays (Saturday or Sunday), so that clients are not inconvenienced
                        and the work proceeds as originally planned.</p>
                    <h2 style="padding-top: 8pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        <a name="bookmark12">List of Holidays</a></h2>
                    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 118%;
                        text-align: left;">
                        The list of holidays (including statutory holidays) will be published by the organization
                        and sent by mail to all employees, before the beginning of each calendar year.</p>
                    <h2 style="padding-top: 8pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        <a name="bookmark13">Time Sheet and Leave</a></h2>
                </li>
                <li data-list-text="">
                    <p style=" padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: justify;">
                        Every new employee will be provided login credentials by <b>Admin </b>for accessing
                        the company’s Time Sheet application or an excel sheet when the Timesheet system
                        is not available.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        Every employee will be entitled to 2 days each of <b>Casual Leave, Privilege Leave and
                            Sick Leave </b>per calendar year</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 112%; text-align: justify;">
                        Maternity Leave of 6 Months will be provided to women employees upon completion
                        of one year of service</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        Depending on the employee’s date of joining the leave entitlement will be on a proportional
                        basis</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: justify;">
                        In case one or more leave categories are not fully availed by the employee, the
                        unavailed portion will get added to the category concerned, the following year,
                        except sick leave and casual leave</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; text-align: justify;">
                        Submission of leave request will also be through Time Sheet application</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: justify;">
                        Employees are expected to apply for and have their leave requests approved by the
                        manager concerned well in advance, before proceeding on leave, except in case of
                        emergency</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        In case of emergency, where advance intimation is not possible, the employee is
                        expected to inform the reporting manager by other available means, so that work
                        is not impacted</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        Employees are expected to login the number of hours worked, on a daily basis, in
                        the said application.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; text-align: justify;">
                        Employee should apply leaves using LMS application;</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 50pt; text-indent: -18pt; text-align: justify;">
                        Employee should not take not more than 3 Permissions per month they can apply max</p>
                    <p style="padding-top: 1pt; padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        2.00 hours a day, more than 2.00 hours will be considered as a half a 0.5 days leave.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        IF permission count is more than 3 in a month then it will be accounted as one day
                        leave for that month.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 114%; text-align: left;">
                        At the end of every month 2 days leave can be credited to each individual (i.e.)
                        0.5(Sick), 0.5(Casual) &amp; 1(Privilege) for confirmed employees. If confirmation
                        gets effected in middle of the month, then 1 day is applicable.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: left;">
                        If the employee is taking leave in long weekend they need to apply\give a prior
                        intimation to their leads. If the employee is taking unplanned leave, by next day
                        itself they need to apply through LMS and get the approvals with the leads.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 112%; text-align: left;">
                        Planned leaves for more than two consecutive days should be applied for approval
                        15 days in advance</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 50pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                        Work from home will be granted on case to case basis upon genuine circumstances
                        and approval of the concerned project manager. No work from home will granted for
                        the 1<span class="s1">st</span> one year of service.</p>
                    <h2 style="padding-left: 50pt; text-indent: 0pt; text-align: left;">
                        <a name="bookmark14">Travel</a></h2>
                </li>
                <li data-list-text="">
                    <p style=" padding-left: 50pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        Any travel related to official work will be borne by the company, in accordance
                        with the company’s travel policy which will be determined at the time of the travel.</p>
                    <%--<p style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                        <span>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <img width="252" height="43" src="data:image/jpg;base64,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" />
                                    </td>
                                </tr>
                            </table>
                        </span>
                    </p>--%>
                </li>
            </ul>
        </li>
        <li data-list-text="">
            <h1 style="padding-top: 1pt; padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark15">6 Infrastructure</a></h1>
            <p style=" padding-left: 23pt; text-indent: 0pt; line-height: 115%;
                text-align: justify;">
                Analytic Brains Technologies Pvt. Ltd is located at Old No.1, New No.1, Sambasivam
                Street, T.Nagar, Chennai, India 600 017</p>
            <p style="padding-left: 23pt; text-indent: 0pt; text-align: justify;">
                The office functions from 1<span class="s1">st</span> floor of the building,</p>
            <ul id="l6">
                <li data-list-text="">
                    <p style="padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        The Floor has 5 rooms as follows ( Two work Station area, Conference room, MD’s
                        Room, Dining room )</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 1pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        MD’s room is occupied by Sundaresan Subramanyan</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 61pt; text-indent: -18pt; line-height: 112%;
                        text-align: left;">
                        A workstation room is currently having 20 workstations, with an attached rest room
                        which is exclusively used by the male employees</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        A dining room, used by all staff for having lunch and other refreshments.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 61pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        There is a rest room inside the dining room, which is used exclusively by the female
                        employees</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        The dining room is also equipped with a Refrigerator, for use by employees</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 61pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        When we are entering the office there is an open verandah, which is used as reception
                        area.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        Coffee/ Tea is served to all employees twice a day (morning and evening)</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 61pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        Every employee is expected to make careful and optimum use of the infrastructure
                        available</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        The entire office is fully air conditioned except dining and rest rooms</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 61pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        In the event of power failure, there is a power backup to provide uninterrupted
                        power supply for the computers</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        At the end of day, the employee leaving office last is required to ensure that all
                        Air conditioners and fans are switched off</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        One set of key is available with building owner.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 61pt; text-indent: -18pt; line-height: 112%;
                        text-align: left;">
                        In short, all employees are expected to use and gently handle the available infrastructure
                        as they would use their own</p>
                </li>
            </ul>
        </li>
        <li data-list-text="">
            <h1 style="padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark16">7 Dress Code</a></h1>
            <p style=" padding-left: 23pt; text-indent: 0pt; line-height: 114%;
                text-align: justify;">
                Employees should dress and behave appropriately. In order to create and maintain
                a professional image, employees are expected to wear clothing that is not offensive
                or distracting to clients and colleagues. This includes maintaining appropriate
                and commonly accepted standards of dressing and grooming that reflects a professional
                and business like image at all times.</p>
            <p style="padding-left: 23pt; text-indent: 0pt; line-height: 115%; text-align: justify;">
                The general principle for adopting business casual dress is related to creating
                a positive image of the Company before our customers and colleagues.</p>
            <%--<p style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                <span>
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <img width="256" height="44" src="data:image/jpg;base64,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" />
                            </td>
                        </tr>
                    </table>
                </span>
            </p>--%>
            <ul id="l7">
                <li data-list-text="">
                    <p style="padding-top: 3pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        Remember that at all times in the workplace employees are ambassadors of the Company</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; line-height: 112%;
                        text-align: left;">
                        Clothes should not cause embarrassment, or have a negative impact upon the image
                        of the Company</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 61pt; text-indent: -18pt; line-height: 113%; text-align: left;">
                        Employees representing the Company onshore/off‐site with third party companies or
                        clients should wear business formals</p>
                    <h3 style="padding-left: 23pt; text-indent: 0pt; text-align: left;">
                        Dress Code: <span class="p">As per the company policy the dress code for everyone is
                            mentioned below:</span></h3>
                </li>
                <li data-list-text="">
                    <h3 style="padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        <b>Monday and Tuesday – Business Formals</b></h3>
                </li>
                <li data-list-text="">
                    <h3 style="padding-top: 2pt; padding-left: 61pt; text-indent: -18pt; text-align: left;">
                        <b>Wednesday through Friday - Business Casuals</b></h3>
                    <p style="padding-left: 23pt; text-indent: 0pt; text-align: left;">
                        The intent of the policy is to create a comfortable work environment that is also
                        professional and free from distractions. Business Casual is defined as clean, neat
                        and professional in appearance.</p>
                    <p style="padding-left: 23pt; text-indent: 0pt; text-align: left;">
                        Below are some general guidelines for both men and women regarding what attire is
                        appropriate.</p>
                    <table style="border-collapse: collapse; margin: 10px auto 10px 50px;" cellspacing="0">
                        <tr style="height: 14pt">
                            <td style="width: 92pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s2" style="padding-left: 5pt; text-indent: 0pt; line-height: 12pt; text-align: left;">
                                    Clothing</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s2" style="padding-left: 5pt; text-indent: 0pt; line-height: 12pt; text-align: left;">
                                    Appropriate</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s2" style="padding-left: 5pt; text-indent: 0pt; line-height: 12pt; text-align: left;">
                                    Not Appropriate</p>
                            </td>
                        </tr>
                        <tr style="height: 41pt">
                            <td style="width: 92pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Formals</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Shirt/ Trouser/ Indian Wear/ Business Suit/ Mid length skirt</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Bicycle pants / Athletic pants/</p>
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; line-height: 13pt; text-align: left;">
                                    Army fatigues overalls/ low rise jeans or pants jeans</p>
                            </td>
                        </tr>
                        <tr style="height: 41pt">
                            <td style="width: 92pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Casual</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Golf/ Polo shirts/ T shirts (without slogans or pictures), suits and</p>
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; line-height: 13pt; text-align: left;">
                                    skirts</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Sweat shirts, Rugby shirts, Mini shirts, Short dresses</p>
                            </td>
                        </tr>
                        <tr style="height: 68pt">
                            <td style="width: 92pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Shoes</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; text-align: left;">
                                    Male – Formal Black or Brown shoes (Leather/ Suede)</p>
                                <p style="text-indent: 0pt; text-align: left;">
                                </p>
                                <p class="s3" style="padding-left: 5pt; text-indent: 0pt; line-height: 14pt; text-align: left;">
                                    Female – Peep – toes, Closed shoes, Buckled Sandals</p>
                            </td>
                            <td style="width: 163pt; border-top-style: solid; border-top-width: 1pt; border-left-style: solid;
                                border-left-width: 1pt; border-bottom-style: solid; border-bottom-width: 1pt;
                                border-right-style: solid; border-right-width: 1pt">
                                <p class="s3" style="padding-left: 5pt; padding-right: 1pt; text-indent: 0pt; text-align: left;">
                                    Flip flops/ Slip on, Sneakers Sandals &amp; Floaters</p>
                            </td>
                        </tr>
                    </table>
                    <p style="padding-top: 2pt; padding-left: 23pt; text-indent: 0pt; text-align: justify;">
                        Please note that these are general guidelines and do not include every appropriate
                        or inappropriate item. We believe employees would exercise good judgment. While
                        we recognize and respect that dress is a personal choice, we do want employees to
                        understand that their choices in these areas have consequences. In the workplace,
                        it is critical that our choices reflect our commitment to the Blue Box Values, or
                        Company Code of Conduct, and our commitment to one another and our customers.</p>
                    <p style="padding-left: 23pt; text-indent: 0pt; text-align: justify;">
                        ID card: Employee should wear ID card inside the office at all times</p>
                </li>
            </ul>
        </li>
        <li data-list-text="">
            <h1 style="padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark19">8 Professionalism and Discipline</a></h1>
            <p style="padding-top: 2pt; padding-left: 23pt; text-indent: 0pt; line-height: 114%;
                text-align: justify;">
                When representing the organization, employees should dress and behave appropriately.
                Excessive use of profanity is neither professional nor respectful to clients or
                co-workers and will not be tolerated.</p>
            <p style="padding-left: 23pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
                Discipline at the organization shall be progressive, depending on the nature of
                the problem. Its purpose is to identify unsatisfactory performance and / or unacceptable
                behaviour. The stages may be:</p>
            <ul id="l8">
                <li data-list-text="">
                    <p style=" padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Verbal reprimand</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 1pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Written reprimand</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 59pt; text-indent: -18pt; text-align: left;">
                        Dismissal</p>
                    <p style="padding-top: 12pt; padding-left: 23pt; text-indent: 0pt; line-height: 115%;
                        text-align: left;">
                        Some circumstances may be serious enough that all three steps are not used. Some
                        examples of these types of situations are theft, assault or wilful neglect of duty.</p>
                </li>
            </ul>
        </li>
        <li data-list-text="">
            <h1 style="padding-left: 41pt; text-indent: -18pt; text-align: left;">
                <a name="bookmark20">9 Business Communication</a></h1>
            <ul id="l9">
            <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 53pt; margin-left: -24px; text-align: left;">
                        English language should be used for all official communication in the organization
                        during business hours.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 53pt; margin-left: -24px; text-align: left;">
                        Employees will be provided an Microsoft account for business purposes</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 36pt; text-indent: -18pt; text-align: left;">
                        Microsoft teams can be used as an instant messaging tool internal to the organization.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 4pt; padding-left: 35pt; text-indent: -18pt; text-align: left;">
                        Microsoft Outlook email is the official email communication tool for the organization</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 3pt; padding-left: 35pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        Microsoft teams / Zoom can be used for internal communication and for client communication
                        (depending on client preferences).</p>
                </li>
            </ul>
        </li>
        <li data-list-text="">
            <h1 style="padding-left: 42pt; text-indent: -19pt; text-align: left;">
                <a name="bookmark21">10 InternetUsage</a></h1>
            <p style="padding-left: 23pt; text-indent: 0pt; line-height: 115%; text-align: left;">
                There are no restrictions on the usage of internet at the office. However, with
                freedom comes responsibility. Employees are expected to:</p>
            <ul id="l10">
                <li data-list-text="">
                    <p style=" padding-left: 53pt; text-indent: -18pt; text-align: left;">
                        Access Internet, at office, solely for work related purposes</p>
                </li>
                <li data-list-text="">
                    <p style="padding-top: 2pt; padding-left: 53pt; text-indent: -18pt; line-height: 113%;
                        text-align: left;">
                        Do Not download files, apps that are NOT required for work and that are a potential
                        threat to IT security</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 53pt; text-indent: -18pt; line-height: 113%; text-align: left;">
                        Spend useful work hours on browsing new technologies, trends, technical solutions
                        for problems, etc. on the internet.</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 53pt; text-indent: -18pt; line-height: 113%; text-align: left;">
                        Completely avoid social media unless it is for professional interaction which furthers
                        the organization’s benefits</p>
                </li>
                <li data-list-text="">
                    <p style="padding-left: 53pt; text-indent: -18pt; line-height: 112%; text-align: left;">
                        Usage of internet that leads to non-productivity is strictly prohibited and may
                        result in disciplinary action depending on the type and content of usage.</p>
                </li>
            </ul>
        </li>
    </ol>
    <h1 style="padding-top: 2pt; padding-left: 23pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark22">11 Confidentiality</a></h1>
    <h2 style=" padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark23">Confidential Information</a></h2>
    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 114%;
        text-align: justify;">
        From time to time, employees of the organization may come into contact with confidential
        information, including but not limited to information about the organization’s customers,
        partners, finances and business plans. Employees are required to keep any such matters
        strictly confidential.</p>
    <p style="padding-left: 41pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
        Furthermore, any such confidential information, obtained through employment with
        the organization must not be used by an employee for personal gain or to further
        an outside enterprise.</p>
    <h2 style="padding-top: 8pt; padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark24">Intellectual Property</a></h2>
    <p style="padding-left: 41pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
        Any intellectual property, such as trademarks, copyrights and patents, and any work
        created by an employee in the course of employment at the organization shall be
        the property of the organization and the employee is deemed to have waived all rights
        in favour of the organization. Work, for the purpose of this policy refers to written,
        creative or media work. All source material used in presentation or documents in
        any devise must be duly acknowledged and accounted for. Office consumables such
        as stationery/ printer etc should be used strictly for official purposes.</p>
    <h2 style="padding-top: 8pt; padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark25">IT Information Storage and Security</a></h2>
    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 114%;
        text-align: justify;">
        Any storage devices (CD’s, USB’s, Floppy Discs) used by employees at Analytic Brains
        Technologies Pvt. Ltd are the property of the organization. Furthermore, it should
        be understood by employees, that company equipment should be used for company business
        only during normal working hours. Downloading of personal materials on company equipment
        can be harmful to the said equipment and is strictly prohibited. Installation of
        any software/apps in workstations without prior approval is also prohibited.</p>
    <h1 style="padding-left: 23pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark26">12 Health and Safety</a></h1>
    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 113%;
        text-align: justify;">
        The Organization, along with its employees, must take reasonable precautions to
        ensure that the workplace is safe. The organization complies with all requirements
        for creating a healthy and safe workplace.</p>
    <p style="padding-top: 2pt; padding-left: 41pt; text-indent: 0pt; line-height: 115%;
        text-align: justify;">
        Employees who have health and safety concerns or identify potential hazards should
        promptly inform the same to Admin and/ or their managers concerned.</p>
    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 114%;
        text-align: justify;">
        Upon completion of one year of service, employees will be enrolled in the company’s
        group insurance policy as per prevailing norms.</p>
    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 115%;
        text-align: justify;">
        In Pandemic situations, social distancing and wearing of face masks is compulsory
        wherever applicable. Employees are encouraged to follows all safety protocols recommended
        by the company and the government. The organization will put in place necessary
        safety measures depending on the situation.</p>
    <h2 style="padding-top: 8pt; padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark27">Alcohol Consumption and Illegal Drug Use</a></h2>
    <p style="padding-left: 46pt; text-indent: 0pt; text-align: justify;">
        Alcohol consumption or illegal drug use is not permitted during work hours on the
        office premises.</p>
    <h2 style="padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark28">Air Quality and Smoke Free Environment</a></h2>
    <p style="padding-left: 46pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
        Indoor air quality can lead to many health issues. The organization recognizes this
        and attempts to minimize the risks associated with indoor air quality and the effects
        on its employees. Issues pertaining to air quality should be promptly reported to
        the Admin<span class="s4">.</span></p>
    <p style="padding-left: 46pt; text-indent: 0pt; text-align: justify;">
        Smoking inside the office premises is not permitted at any time.</p>
    <h2 style="padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark29">Harassment</a></h2>
    <p style="padding-left: 46pt; text-indent: 0pt; line-height: 113%; text-align: justify;">
        The organization wants to provide a harassment-free environment for its employees.
        Mutual respect, along with cooperation and understanding, must be the basis of interaction
        between employees among themselves and with their reporting managers. The organization
        will neither tolerate, nor condone behaviour that is likely to undermine the dignity
        or self-esteem of an individual, or create an intimidating, hostile or offensive
        environment.</p>
    <p style="padding-left: 46pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
        There are several forms of harassment but all can be defined as any unwelcome action
        by any person, whether verbal or physical, on a single or repeated basis, which
        humiliates insults or degrades. “Unwelcome”, for the purposes of this policy, refers
        to any action which the harasser knows, or ought to reasonably know is not desired
        by the victim of the harassment.</p>
    <p style="padding-top: 2pt; padding-left: 46pt; text-indent: 0pt; line-height: 114%;
        text-align: justify;">
        Specifically, racial harassment is defined as any unwelcome comments, racist statements,
        slurs, jokes, graffiti or literature or pictures and posters which may intentionally
        or unintentionally offend another person.</p>
    <p style="padding-left: 46pt; text-indent: 0pt; line-height: 113%; text-align: justify;">
        Sexual harassment is any unwanted attention of a sexual nature such as remarks about
        appearance or personal life, offensive written or visual actions like graffiti or
        degrading pictures, physical contact of any kind, or sexual demands.</p>
    <h2 style="padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark30">Workplace Violence and Abuse</a></h2>
    <p style=" padding-left: 46pt; text-indent: 0pt; line-height: 113%;
        text-align: justify;">
        Workplace violence can be defined as a threat or an act of aggression resulting
        in physical or psychological damage, pain or injury to a worker, which arises during
        the course of work.</p>
    <p style="padding-left: 46pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
        Abuse can be verbal, psychological or sexual in nature. Verbal abuse is the use
        of unwelcome, embarrassing, offensive, threatening or degrading comments. Psychological
        abuse is an act which provokes fear or diminishes a person’s dignity or self-esteem.
        Finally, sexual abuse is any unwelcome verbal or physical advance or sexually explicit
        statement.</p>
    <p style="padding-left: 46pt; text-indent: 0pt; line-height: 113%; text-align: justify;">
        The organization has a zero tolerance limit with regards to harassment and violence.
        Employees or volunteers engaging in either harassing or violent activities will
        be subject to discipline, which may include termination of employment and possibly
        criminal charges.</p>
    <h1 style="padding-left: 23pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark31">13 Dispute Resolution</a>
    </h1>
    <p style=" padding-left: 41pt; text-indent: 0pt; line-height: 114%;
        text-align: justify;">
        Regrettably, conflict can occur in any working environment. In an effort to resolve
        conflict in an expedient, yet fair manner, the organization recommends the following
        process for conflict or dispute resolution.</p>
    <ul id="l11">
        <li data-list-text="">
            <p style="padding-left: 95pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                Speak to the person you are having the dispute with. Many times disputes arise due
                to misunderstandings and miscommunications.</p>
        </li>
        <li data-list-text="">
            <p style="padding-left: 95pt; text-indent: -18pt; line-height: 113%; text-align: justify;">
                If speaking to the individual does not work, speak to the Managing Director. The
                MD will arrange a meeting between those involved in the dispute, to determine a
                resolution.</p>
        </li>
        <li data-list-text="">
            <p style="padding-left: 95pt; text-indent: -18pt; text-align: justify;">
                The resolution of the MD shall be binding on both parties of the dispute.</p>
            <h1 style="padding-top: 2pt; padding-left: 23pt; text-indent: 0pt; text-align: left;">
                <a name="bookmark32">14 Departure</a></h1>
            <p style=" padding-left: 41pt; text-indent: 0pt; text-align: left;">
                An employee may leave the organization for two reasons:</p>
        </li>
        <li data-list-text="">
            <p style="padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Resignation</p>
        </li>
        <li data-list-text="">
            <p style="padding-top: 2pt; padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Termination by employer</p>
            <h2 style=" padding-left: 68pt; text-indent: 0pt; text-align: left;">
                <a name="bookmark33">Resignation by Employee</a></h2>
            <p style="padding-left: 46pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
                Employees who choose to resign are expected to give the organization notice period
                as specified in the appointment letter or subsequent revision letters. The Employer
                may waive the resignation notice period in whole or in part at any time, by providing
                payment of regular wages for the period so waived.</p>
            <p style="padding-left: 46pt; text-indent: 0pt; line-height: 114%; text-align: justify;">
                The employee will be issued an experience letter at the time of departure upon satisfactory
                hand over of company materials as required by the concerned reporting manager and
                management.</p>
            <p style="padding-left: 46pt; text-indent: 0pt; line-height: 115%; text-align: justify;">
                Full and final settlement will be done for the employee by the accounts department
                in 30 to 45 days from last date of service in the organization.</p>
            <h2 style="padding-top: 8pt; padding-left: 68pt; text-indent: 0pt; text-align: left;">
                <a name="bookmark34">Termination by Employer</a></h2>
            <p style="padding-left: 46pt; text-indent: 0pt; line-height: 113%; text-align: justify;">
                An Employment Contract may be terminated by the Employer at any time for cause,
                without notice or payment in lieu of notice or severance pay whatsoever, except
                payment of outstanding wages up to the date of termination. Cause includes, but
                is not limited to:</p>
        </li>
        <li data-list-text="">
            <p style="padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Any act of dishonesty</p>
        </li>
        <li data-list-text="">
            <p style="padding-top: 2pt; padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Conflict of interest</p>
        </li>
        <li data-list-text="">
            <p style="padding-top: 2pt; padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Breach of confidentiality</p>
        </li>
        <li data-list-text="">
            <p style="padding-top: 2pt; padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Harassment</p>
        </li>
        <li data-list-text="">
            <p style="padding-top: 1pt; padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Insubordination, or</p>
        </li>
        <li data-list-text="">
            <p style="padding-top: 2pt; padding-left: 95pt; text-indent: -18pt; text-align: left;">
                Careless, negligent, or poor work performance</p>
        </li>
    </ul>

    <h2 style=" padding-left: 68pt; text-indent: 0pt; text-align: left;">
        <a name="bookmark35">Employer Property</a></h2>
    <p style="padding-left: 46pt; text-indent: 0pt; line-height: 113%; text-align: justify;">
        Upon termination of employment for any reason, all items of any kind created or
        used pursuant to the employee’s service, or furnished by the Employer including
        but not limited to computers, reports, files, diskettes, manuals, literature, confidential
        information, or other materials shall remain and be considered the exclusive property
        of the Employer at all times, and shall be surrendered to the Manager concerned,
        in good condition, promptly and without being requested to do so.</p>
         <h1 style="padding-left: 40%; text-indent: 0pt; text-align: left;">Thank You!!!
      </h1>
</asp:Content>
