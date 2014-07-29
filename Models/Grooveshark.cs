using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RestSharp;

namespace FavoriteSongs.Models
{
    public class Grooveshark
    {
        private const string ServiceUrl = "https://api.grooveshark.com";

        private const string WsKey ;
        private const string Secret ;
        
        public static Dictionary<string, object> Authenticate(string sessionId, User user)
        {
            var headers = new Dictionary<string, string>();
            headers.Add("secret", Secret);
            headers.Add("sessionID", sessionId);

            var parameters = new Dictionary<string, object>();
            parameters.Add("login", user.UserName);
            parameters.Add("password", Encryptor.CalculateMD5Hash(user.Password));

            var responseParameters = SendRequest("authenticate", headers, parameters);
            return responseParameters.result;
        }

        public static string StartSession()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("secret", Secret);

            var responseParameters = SendRequest("startSession", headers);
            return responseParameters.result["sessionID"].ToString();
        }

        public static Dictionary<string, object> GetUserFavoriteSongs(string sessionId)
        {
            var headers = new Dictionary<string, string> { { "sessionID", sessionId } };

            return Grooveshark.SendRequest("getUserFavoriteSongs", headers).result;
        }

        public static ResponseParameters SendRequest(string method, Dictionary<string, string> header, Dictionary<string, object> parameters = null)
        {
            var requestParameters = new RequestParameters { method = method, header = header, parameters = parameters };
            requestParameters.header.Add("wsKey", WsKey);

            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(requestParameters);
            var encryptedJson = Encryptor.Md5Encrypt(json, Secret);

            var client = new RestClient(ServiceUrl);
            var request = new RestRequest(String.Format("/ws3.php?sig={0}", encryptedJson.ToLower()), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(requestParameters);

            var response = (RestResponse)client.Execute(request);
            var responseParameters = jsonSerializer.Deserialize<ResponseParameters>(response.Content);
            return responseParameters;
        }
    }
}
