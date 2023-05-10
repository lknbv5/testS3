using System;
using System.IO;
using System.Net;
using System.Web.Http;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3.Controllers
{
    [RoutePrefix("Test")]
    public class TestController : ApiController
    {
        const string accessKeyId = "C598D25DB217E_RWLT";
        const string accessKeySecret = "w5u3sRWLT";
        const string endpoint = "http://10.30.3.15:8333";

        private static IAmazonS3 s3Client;
        [HttpGet, Route("test")]
        public dynamic Download()
        {
            
            string modelId = "05693c5e-565e-4adc-8369-4b1130256990";
            var s3ClientConfig = new AmazonS3Config
            {
                ServiceURL = endpoint,
                SignatureVersion = "4",
                UseHttp = true,
            };
            s3Client = new AmazonS3Client(accessKeyId, accessKeySecret, s3ClientConfig);
            //var request = new GetObjectRequest()
            //{
            //    BucketName = "project-model",
            //    Key = modelId + ".zip"
            //};
            //var r = s3Client.GetObject(request);
            //r.WriteResponseStreamToFile($"E:/桌面/{modelId}.zip");
            var requesturl = new GetPreSignedUrlRequest()
            {
                Protocol= Protocol.HTTP,
                Verb = HttpVerb.PUT,
                BucketName = @"project-model",
                Key = "abc/test.jpg",
                Expires = DateTime.UtcNow.AddMinutes(5)
            };
            var url=s3Client.GetPreSignedURL(requesturl);
            //var filePath = "E://桌面//test.jpg";
            //HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            //httpRequest.Method = "PUT";
            //using (Stream dataStream = httpRequest.GetRequestStream())
            //{
            //    var buffer = new byte[8000];
            //    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            //    {
            //        int bytesRead = 0;
            //        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            //        {
            //            dataStream.Write(buffer, 0, bytesRead);
            //        }
            //    }
            //}
            //HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse;
            return url;
        }
    }
}
