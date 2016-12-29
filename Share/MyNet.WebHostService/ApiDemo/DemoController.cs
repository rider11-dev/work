using MyNet.Components.Emit;
using MyNet.WebHostService.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace MyNet.WebHostService.ApiDemo
{
    [RoutePrefix("api/demo")]
    public class DemoController : ApiController
    {
        public Contact Get([ModelBinder]Contact contact)
        {
            return contact;
        }

        [Route("val")]
        [HttpGet]
        public Tuple<string[], string[]> Val(Contact model)
        {
            HttpActionContext actionContext = new HttpActionContext { ControllerContext = this.ControllerContext };
            ModelMetadataProvider metadataProvider = this.Configuration.Services.GetModelMetadataProvider();
            //创建针对目标类型的空对象
            var ins = DynamicModelBuilder.GetInstance<IContact>(propAttrProvider: () =>
           {
               return new List<PropertyCustomAttributeUnit>
               {
                    new PropertyCustomAttributeUnit
                    {
                        prop_name="phone",
                        attrs=new List<PropertyCustomAttribute>
                        {
                            new PropertyCustomAttribute
                            {
                                attr_type="System.ComponentModel.DataAnnotations.RequiredAttribute,System.ComponentModel.DataAnnotations, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
                                ctor_arg_types="",
                                ctor_arg_values=new object[0],
                                error_msg="姓名不能为空",
                                error_msg_res_name="",
                                error_msg_res_type=""
                            }
                        }
                    }
               };
           });
            ModelMetadata metadata = metadataProvider.GetMetadataForType(null, ins.GetType());
            IValueProvider valueProvider = new CompositeValueProviderFactory(this.Configuration.Services.GetValueProviderFactories()).GetValueProvider(actionContext);
            ModelBindingContext bindingContext = new ModelBindingContext { ModelMetadata = metadata, ValueProvider = valueProvider, ModelState = actionContext.ModelState };

            string[] errMsg1 = actionContext.ModelState.SelectMany(item => item.Value.Errors.Select(err => err.ErrorMessage)).ToArray();
            bindingContext.ValidationNode.ValidateAllProperties = true;
            bindingContext.ValidationNode.Validate(actionContext);
            string[] errMsg2 = actionContext.ModelState.SelectMany(item => item.Value.Errors.Select(err => err.ErrorMessage)).ToArray();
            return new Tuple<string[], string[]>(errMsg1, errMsg2);
        }
    }
}
