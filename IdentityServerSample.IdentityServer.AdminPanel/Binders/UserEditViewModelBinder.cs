using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServerSample.IdentityServer.AdminPanel.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace IdentityServerSample.IdentityServer.AdminPanel.Binders
{
    public class UserEditViewModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProvider = bindingContext.ValueProvider;

            var model = new UserEditViewModel();

            model.Id = valueProvider.GetValue("Id").FirstValue;

            model.UserName = valueProvider.GetValue("UserName").FirstValue;

            model.ClaimAmount = int.Parse(valueProvider.GetValue("ClaimAmount").FirstValue);

            if (valueProvider.ContainsPrefix("Claims"))
            {
                model.Claims = new List<ClaimViewModel>();

                for (int i = 0; i < model.ClaimAmount; i++)
                {
                    var id = int.Parse(valueProvider.GetValue($"Claims[{i}].Id").FirstValue);
                    var type = valueProvider.GetValue($"Claims[{i}].Type").FirstValue;
                    var value = valueProvider.GetValue($"Claims[{i}].Value").FirstValue;
                    model.Claims.Add(new ClaimViewModel(id, type, value));
                }
            }            

            bindingContext.Model = model;
            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }

    public class UserEditViewModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(UserEditViewModel))
            {
                return new BinderTypeModelBinder(typeof(UserEditViewModelBinder));
            }

            return null;
        }
    }
}
