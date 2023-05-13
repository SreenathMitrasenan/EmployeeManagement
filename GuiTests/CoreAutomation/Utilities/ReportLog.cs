using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAutomation.Utilities
{
    public static class ReportLog
    {
        private static readonly DataTable _table = new DataTable();
        private static readonly object _lock = new object();
        private static int stepNumber = 0;

        static ReportLog()
        {
            _table.Columns.Add("Step Number", typeof(int));
            _table.Columns.Add("Step Description", typeof(string));
            _table.Columns.Add("Status", typeof(string));
        }

        public static void ReportStep(Status status, string stepDescription)
        {
            lock (_lock)
            {
                stepNumber++;
                _table.Rows.Add(stepNumber, stepDescription, status.ToString());
            }
        }

        public static void Clear()
        {
            lock (_lock)
            {
                _table.Rows.Clear();
                stepNumber = 0;
            }
        }

        public static DataTable FlushLogDt()
        {
            lock (_lock)
            {
                return _table.Copy();
            }
        }

        public static string GetLogTable()
        {
            if (_table.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style=\"position: relative;\">");
            sb.Append("<table id=\"logTable\" style=\"border: 2px background-color: #2e8b57; table-layout: fixed; width: 100%;\"><tr style=\"background-color: #2e8b57; color: black; height: 12px; line-height: 12px;\"><th style=\"width: 6%; height: 12px; line-height: 12px;\">No.</th><th style=\"height: 12px; line-height: 12px;\">Step Description</th><th style=\"width: 6%; height: 12px; line-height: 12px;\">Status</th></tr>");


            lock (_lock)
            {
                foreach (DataRow row in _table.Rows)
                {
                    string rNum     =   row["Step Number"].ToString();
                    string step     =   row["Step Description"].ToString();
                    string status   =   row["Status"].ToString();
                    sb.Append("<tr style=\"height: 10px; line-height: 10px;\">");
                    
                    
                    if (status.Equals("Pass"))
                    {
                        sb.Append("<td><span style=\"font-size: 1.5em;\">&#8226;&nbsp;</span><span style=\"font-size: 0.9em;\">" + rNum + " </span></td>");
                        sb.Append("<td><span style=\"font-size: 0.9em;\">" + step + "</span></td>");
                        sb.Append("<td style=\"width: 6%; color: #32cd32;\">" + status + "</td>");
                    }
                    else if (status.Equals("Fail"))
                    {
                        sb.Append("<td><span style=\"font-size: 1.5em;\">&#8226;&nbsp;</span><span style=\"font-size: 0.9em;\">" + rNum + " </span></td>");
                        sb.Append("<td><span style=\"font-size: 0.9em;\">" + step + "</span></td>");
                        sb.Append("<td style=\"width: 6%; color: red;\">" + status + "</td>");
                    }
                    else
                    {
                        sb.Append("<td><span style=\"font-size: 1.5em; color: #2196F3;\">&#8226;&nbsp;</span><span style=\"font-size: 0.9em;\">" + rNum + " </span></td>");
                        sb.Append("<td><span style=\"font-size: 0.9em; color: #2196F3; \">" + step + "</span></td>");
                        sb.Append("<td style=\"width: 6%; color: #ffff00;\">" + status + "</td>");
                    }
                    sb.Append("</tr>");
                }
                _table.Rows.Clear();
            }
            sb.Append("</table>");
            sb.Append("</div>");

            return sb.ToString();
        }

        public enum Status
        {
            Pass,
            Fail,
            Info,
            Skip
        }

    
    }
}
