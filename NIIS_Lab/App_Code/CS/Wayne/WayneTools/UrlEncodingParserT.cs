using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace WayneTools
{
    public class UrlEncodingParserT : NameValueCollection
    {

        private string Url { get; set; }

        public bool DecodePlusSignsAsSpaces { get; set; }

        public UrlEncodingParserT(string queryStringOrUrl = null, bool decodeSpacesAsPlusSigns = false)
        {
            Url = string.Empty;
            DecodePlusSignsAsSpaces = decodeSpacesAsPlusSigns;
            if (!string.IsNullOrEmpty(queryStringOrUrl))
            {
                Parse(queryStringOrUrl);
            }
        }

        public void SetValues(string key, IEnumerable<string> values)
        {
            foreach (var val in values)
                Add(key, val);
        }

        public NameValueCollection Parse(string query)
        {
            if (Uri.IsWellFormedUriString(query, UriKind.Absolute))
                Url = query;

            if (string.IsNullOrEmpty(query))
                Clear();
            else
            {
                int index = query.IndexOf('?');
                if (index > -1)
                {
                   
                    if (query.Length >= index + 1)
                        query = query.Substring(index + 1);
                }

                var pairs = query.Split('&');

                foreach (var pair in pairs)
                {
                    int index2 = pair.IndexOf('=');
                    if (index2 > 0)
                    {
                        var val = pair.Substring(index2 + 1);

                        if (!string.IsNullOrEmpty(val))
                        {
                            if (DecodePlusSignsAsSpaces)
                                val = val.Replace("+", " ");
                            val = Uri.UnescapeDataString(val);
                        }

                        Add(pair.Substring(0, index2), val);
                    }
                }
            }

            return this;
        }

        public override string ToString()
        {
            string query = string.Empty;
            foreach (string key in Keys)
            {
                string[] values = GetValues(key);
                if (values == null)
                    continue;

                foreach (var val in values)
                {
                    query += key + "=" + Uri.EscapeUriString(val) + "&";
                }
            }
            query = query.Trim('&');

            if (!string.IsNullOrEmpty(Url))
            {
                if (Url.Contains("?"))
                    query = Url.Substring(0, Url.IndexOf('?') + 1) + query;
                else
                    query = Url + "?" + query;
            }

            return query;
        }
    }
}