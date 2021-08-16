
using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace store.Utils.Common
{
    public class ServerResponse<T>
    {

        public T data;
        public IDictionary<string, string> details;



        public ServerResponse()
        {
            this.details = new Dictionary<string, string>();
        }


        public void setMessage(string message)
        {
            this.details.Add("message", message);
        }
        public void setErrorMessage(string message)
        {
            this.details.Add("errorMessage", message);
        }


        public void mapDetails(ValidationResult result)
        {
            IDictionary<string, string> details = new Dictionary<string, string>();


            foreach (var failure in result.Errors)
            {
                string value = "";
                bool isExisted = details.TryGetValue(failure.PropertyName, out value);
                if (!isExisted)
                {
                    details.Add(failure.PropertyName, failure.ErrorMessage);
                }

            }

            this.details = details;
        }


        public IDictionary<string, Object> getResponse()
        {
            IDictionary<string, Object> res = new Dictionary<string, Object>();

            if (this.data == null)
            {
                res.Add("data", null);
            }
            else
            {
                res.Add("data", this.data);
            }



            res.Add("details", this.details);

            return res;

        }

    }
}