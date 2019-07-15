using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using New.Model.User;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
namespace New.Model.Binder
{
    public class AuthorEntityBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            //var key = bindingContext.ModelName;
            //if (string.IsNullOrWhiteSpace(key))
            //    key = bindingContext.FieldName;
            //string val = bindingContext.HttpContext.Request.Form[key];
            //if (string.IsNullOrWhiteSpace(val))
            //    val = bindingContext.HttpContext.Request.Query[key];
            //if (string.IsNullOrWhiteSpace(val))
            //    return Task.CompletedTask;
            //if (bindingContext.ModelType == typeof(int))
            //{
            //    bindingContext.Model = int.Parse(val);
            //}
            //if (bindingContext.Model != null)
            //{
            //    bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            //}
            //if (bindingContext.ModelType == typeof(FormFile))
            //{
            //    bindingContext.Model = bindingContext.HttpContext.Request.Form.Files[0];
            //}
            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();
                var modelType = bindingContext.ModelType;
                //bindingContext.Model = body;// JsonConvert.DeserializeObject<Author>(body);
                //bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
                bindingContext.Result = ModelBindingResult.Success(body);
            }
            return Task.CompletedTask;
        }
    }
}
