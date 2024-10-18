using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace DirecLayer
{
    public class JsonHelper
    {
        public static StringBuilder JsonBuilder(IDictionary<string, string> payload)
        {
            int currentCount = 1;

            char q = '"';

            StringBuilder json = new StringBuilder();

            json.Append("{");

            foreach (var arr in payload)
            {
                if (arr.Value == null || string.IsNullOrEmpty(arr.Value.ToString()))
                {
                }
                else
                {
                    json.Append($"{q}{arr.Key}{q}:{q}{arr.Value}{q},");
                }


                currentCount++;
            }

            json.Append("}");

            return json;
        }

        public static string GetJsonValue(string json, string value)
        {
            try
            {
                if (json != null)
                {
                    JObject err = JObject.Parse(json);
                    if (err.ToString().Contains("error"))
                    {
                        return $"error : {GetJsonError(err.ToString())}";
                    }
                    else
                    {
                        return (string)err[value];
                    }
                }
                else
                {
                    return "";
                }

            }
            catch
            {
                if (json.Contains("error"))
                {
                    string retJson = GetJsonString(json, "");
                    var sbJson = new StringBuilder();
                    sbJson.Append("{" + retJson + "}}}");
                    return GetJsonError(sbJson.ToString());
                }
                else { return "Operation completed successfully"; }
            }

        }

        public static string GetJsonError(string json)
        {
            JObject err = JObject.Parse(json);
            return (string)err["error"]["message"]["value"];
        }

        public static string GetJsonString(string ret, string tag)
        {
            var startTag = "{";
            int startIndex = ret.IndexOf(startTag) + startTag.Length;
            int endIndex = ret.IndexOf("}", startIndex);
            return ret.Substring(startIndex, endIndex - startIndex);
        }

        public static JToken RemoveEmptyChildren(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                JObject copy = new JObject();
                foreach (JProperty prop in token.Children<JProperty>())
                {
                    JToken child = prop.Value;
                    if (child.HasValues)
                    {
                        child = RemoveEmptyChildren(child);
                    }
                    if (!IsEmpty(child))
                    {
                        copy.Add(prop.Name, child);
                    }
                }
                return copy;
            }
            else if (token.Type == JTokenType.Array)
            {
                JArray copy = new JArray();
                foreach (JToken item in token.Children())
                {
                    JToken child = item;
                    if (child.HasValues)
                    {
                        child = RemoveEmptyChildren(child);
                    }
                    if (!IsEmpty(child))
                    {
                        copy.Add(child);
                    }
                }
                return copy;
            }
            return token;
        }

        static bool IsEmpty(JToken token)
        {
            return (token.Type == JTokenType.Null);
        }

        public static string RemoveNullorEmptyJsonValue(string json)
        {
            JToken token = JsonHelper.RemoveEmptyChildren(JToken.Parse(json));
            return token.ToString(Formatting.Indented);
        }
    }
}
