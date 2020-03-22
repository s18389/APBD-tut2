using System;
using System.Collections.Generic;
using System.Text;

namespace tut2.Models
{
    public class studies
    {
        private string _studiesName;
        private string _studiesMode;

        public string name
        {
            get { return _studiesName; }
            set { _studiesName = value ?? throw new ArgumentException(); }
        }

        public string mode
        {
            get { return _studiesMode; }
            set { _studiesMode = value ?? throw new ArgumentException(); }
        }
    }
}
