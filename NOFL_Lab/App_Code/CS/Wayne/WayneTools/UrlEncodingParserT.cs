﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace WayneTools
{


    //把URL做編碼
    /// <summary>
    /// A query string or UrlEncoded form parser and editor 
    /// class that allows reading and writing of urlencoded
    /// key value pairs used for query string and HTTP 
    /// form data.
    /// 
    /// Useful for parsing and editing querystrings inside
    /// of non-Web code that doesn't have easy access to
    /// the HttpUtility class.                
    /// </summary>
    /// <remarks>
    /// Supports multiple values per key
    /// </remarks>
    public class UrlEncodingParserT : NameValueCollection
    {

        /// <summary>
        /// Holds the original Url that was assigned if any
        /// Url must contain // to be considered a url
        /// </summary>
        private string Url { get; set; }

        /// <summary>
        /// Determines whether plus signs in the UrlEncoded content
        /// are treated as spaces.
        /// </summary>
        public bool DecodePlusSignsAsSpaces { get; set; }

        /// <summary>
        /// Always pass in a UrlEncoded data or a URL to parse from
        /// unless you are creating a new one from scratch.
        /// </summary>
        /// <param name="queryStringOrUrl">
        /// Pass a query string or raw Form data, or a full URL.
        /// If a URL is parsed the part prior to the ? is stripped
        /// but saved. Then when you write the original URL is 
        /// re-written with the new query string.
        /// </param>
        public UrlEncodingParserT(string queryStringOrUrl = null, bool decodeSpacesAsPlusSigns = false)
        {
            Url = string.Empty;
            DecodePlusSignsAsSpaces = decodeSpacesAsPlusSigns;
            if (!string.IsNullOrEmpty(queryStringOrUrl))
            {
                Parse(queryStringOrUrl);
            }
        }


        //設定
        /// <summary>
        /// Assigns multiple values to the same key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public void SetValues(string key, IEnumerable<string> values)
        {
            foreach (var val in values)
                Add(key, val);
        }


        //取得鍵值
        /// <summary>
        /// Parses the query string into the internal dictionary
        /// and optionally also returns this dictionary
        /// </summary>
        /// <param name="query">
        /// Query string key value pairs or a full URL. If URL is
        /// passed the URL is re-written in Write operation
        /// </param>
        /// <returns></returns>
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
                    //檢查開頭是否是?號，如果不是就從?號開始擷取
                    if (query.Length >= index + 1)
                        query = query.Substring(index + 1);
                }

                //開始區分&號
                var pairs = query.Split('&');

                foreach (var pair in pairs)
                {
                    //區分=號
                    int index2 = pair.IndexOf('=');
                    if (index2 > 0)
                    {
                        //擷取等號後面的data
                        var val = pair.Substring(index2 + 1);

                        if (!string.IsNullOrEmpty(val))
                        {
                            //如果url的值有+號就改成空白
                            if (DecodePlusSignsAsSpaces)
                                val = val.Replace("+", " ");
                            val = Uri.UnescapeDataString(val);
                        }

                        //加入key和值
                        Add(pair.Substring(0, index2), val);
                    }
                }
            }

            return this;
        }

        //複寫ToString方法
        /// <summary>
        /// Writes out the urlencoded data/query string or full URL based 
        /// on the internally set values.
        /// </summary>
        /// <returns>urlencoded data or url</returns>
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
                //如果url有?號就只擷取?之前的字串
                //再把所有的queryString加起來
                if (Url.Contains("?"))
                    query = Url.Substring(0, Url.IndexOf('?') + 1) + query;
                else
                    query = Url + "?" + query;
            }

            return query;
        }
    }
}