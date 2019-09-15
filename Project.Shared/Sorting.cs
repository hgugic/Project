using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Project.Shared
{
    public class Sorting : ISort
    {
        public Sorting(string sortAndDirection)
        {
            if(!string.IsNullOrEmpty(sortAndDirection))
            {
                if(sortAndDirection.Contains("_"))
                {
                    string[] str = Regex.Split(sortAndDirection, "_");
                    SortBy = str[0];
                    SortDirection = str[1];
                }
                else
                {
                    SortBy = sortAndDirection;
                }
            }
            
        }

        public string SortBy { get; set; }
        public string SortDirection { get; set; }
    }
}
