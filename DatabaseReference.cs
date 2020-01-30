
/*\
$$$  Project : Firebase
$$$  Author  : Modetor
$$$  Started : 29.1.2020 12:47AM (it's midnight ^_^)
$$$  
\*/
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Modetor.Data.Firebase
{
    public class DatabaseReference
    {
        private readonly string DotJSON = ".json";
        public string URIs { get; private set; }
        public DatabaseReference()
        {
            URIs = FirebaseReference.GetReference().ProjectUri;
        }
        public DatabaseReference(string uri)
        {
            URIs = uri;
        }

        public DatabaseReference Child(string child)
        {
            URIs += child + "/";
            return this;
        }

        public FirebaseEventHandler Put(object obj)
        {
            FirebaseEventHandler handler = new FirebaseEventHandler();
            handler.SetProcess(() =>
            {
                FirebaseOperation operation = new FirebaseOperation();
                try
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    HttpWebRequest request = WebRequest.CreateHttp(URIs.EndsWith(DotJSON) ? URIs : URIs + DotJSON);
                    request.Method = "PUT";
                    request.ContentType = "application/json";
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = buffer.Length;
                    request.Timeout = handler.Timeout;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    WebResponse response = request.GetResponse();
                    operation.OperationResult = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    operation.OperationState = true;
                    operation.ThrownException = null;
                }
                catch (Exception exp)
                {
                    operation.ThrownException = exp;
                    operation.OperationState = false;
                }

                return operation;
            });

            return handler;
        }

        
        public FirebaseEventHandler Post(object obj)
        {
            FirebaseEventHandler handler = new FirebaseEventHandler();
            handler.SetProcess(() =>
            {
                FirebaseOperation operation = new FirebaseOperation();
                try
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    HttpWebRequest request = WebRequest.CreateHttp(URIs.EndsWith(DotJSON) ? URIs : URIs + DotJSON);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Timeout = handler.Timeout;
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    WebResponse response = request.GetResponse();
                    operation.OperationResult = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    operation.OperationState = true;
                    operation.ThrownException = null;
                }
                catch (Exception exp)
                {
                    operation.ThrownException = exp;
                    operation.OperationState = false;
                }

                return operation;
            });

            return handler;
        }


        public FirebaseEventHandler Get(RetrievedDataType r)
        {
            FirebaseEventHandler handler = new FirebaseEventHandler();
            handler.SetProcess(() =>
            {
                FirebaseOperation operation = new FirebaseOperation();
                try
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject("");
                    HttpWebRequest request = WebRequest.CreateHttp((URIs.EndsWith(DotJSON) ? URIs : URIs + DotJSON) + (r == RetrievedDataType.HUMAN_READABLE ? "?print=pretty" : string.Empty));
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    request.Timeout = handler.Timeout;
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    WebResponse response = request.GetResponse();
                    operation.OperationResult = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    operation.OperationState = true;
                    operation.ThrownException = null;
                }
                catch (Exception exp)
                {
                    operation.ThrownException = exp;
                    operation.OperationState = false;
                }

                return operation;
            });

            return handler;
        }


        public FirebaseEventHandler Update(object obj)
        {
            FirebaseEventHandler handler = new FirebaseEventHandler();

            handler.SetProcess(() =>
            {
                FirebaseOperation operation = new FirebaseOperation();
                try
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    HttpWebRequest request = WebRequest.CreateHttp(URIs.EndsWith(DotJSON) ? URIs : URIs + DotJSON);
                    request.Method = "PATCH";
                    request.ContentType = "application/json";
                    request.Timeout = handler.Timeout;
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    WebResponse response = request.GetResponse();
                    operation.OperationResult = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    operation.OperationState = true;
                    operation.ThrownException = null;
                }
                catch (Exception exp)
                {
                    operation.ThrownException = exp;
                    operation.OperationState = false;
                }

                return operation;
            });

            return handler;
        }


        public FirebaseEventHandler Delete()
        {
            FirebaseEventHandler handler = new FirebaseEventHandler();

            handler.SetProcess(() =>
            {
                FirebaseOperation operation = new FirebaseOperation();
                try
                {
                    HttpWebRequest request = WebRequest.CreateHttp(URIs.EndsWith(DotJSON) ? URIs : URIs + DotJSON);
                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.Timeout = handler.Timeout;
                    WebResponse response = request.GetResponse();
                    operation.OperationResult = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    operation.OperationState = true;
                    operation.ThrownException = null;
                }
                catch (Exception exp)
                {
                    operation.ThrownException = exp;
                    operation.OperationState = false;
                }

                return operation;
            });

            return handler;
        }
    }

    public enum RetrievedDataType { JSON, HUMAN_READABLE}
}
