using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Utilities
{
    public class Logger
    {

        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        private readonly DataTable _table = new DataTable();
        private readonly object _lock = new object();
        private int stepNumber = 0;
        private Logger()
        {
            _table.Columns.Add("StepNumber", typeof(int));
            _table.Columns.Add("WebElement", typeof(string));
            _table.Columns.Add("ControlType", typeof(string));
            _table.Columns.Add("Action", typeof(string));
            _table.Columns.Add("Status", typeof(string));
        }

        public static Logger Instance => lazy.Value;

        public void ReportStep( string webElement, string controlType, string action, string status)
        {
            lock (_lock)
            {
                stepNumber++;
                _table.Rows.Add(stepNumber, webElement, controlType, action, status);
                
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _table.Rows.Clear();
                stepNumber = 0;
            }
        }

        public DataTable FlushLogDt()
        {
            lock (_lock)
            {
                return _table.Copy();
            }
        }

     
        
      public string GetLogTable()
        {
            if (_table.Rows.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<div style=\"position: relative;\">");
            sb.Append("<table id=\"logTable\" style=\"border: 2px background-color: #2e8b57; table-layout: fixed; width: 100%;\"><tr style=\"background-color: #2e8b57; color: black; height: 12px; line-height: 12px;\"><th style=\"width: 7%; height: 12px; line-height: 12px;\">SLNo.</th><th style=\"height: 12px; line-height: 12px;\">WebElement</th><th style=\"height: 12px; line-height: 12px;\">ControlType</th><th style=\"height: 12px; line-height: 12px;\">Action</th><th style=\"width: 6%; height: 12px; line-height: 12px;\">Status</th></tr>");
            lock (_lock)
            {
                foreach (DataRow row in _table.Rows)
                {
                    sb.Append("<tr style=\"height: 10px; line-height: 10px;\">");
                    sb.Append("<td style=\"width: 7%;\">" + row["StepNumber"] + "</td>");
                    sb.Append("<td>" + row["WebElement"] + "</td>");
                    sb.Append("<td>" + row["ControlType"] + "</td>");
                    sb.Append("<td>" + row["Action"] + "</td>");

                    string status = row["Status"].ToString();
                    if (status.Equals("Pass"))
                    {
                        sb.Append("<td style=\"width: 6%; color: #32cd32;\">" + status + "</td>");
                    }
                    else if (status.Equals("Fail"))
                    {
                        sb.Append("<td style=\"width: 6%; color: red;\">" + status + "</td>");
                    }
                    else
                    {
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





    }

}

 
