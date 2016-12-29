using MyNet.Components.Emit;
using MyNet.WebHostService.ApiDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace MyNet.WebHostService.Extension
{
    public class MyMutableObjectModelBinder : IModelBinder
    {
        internal static bool CanBindType(Type modelType)
        {
            if (TypeDescriptor.GetConverter(modelType).CanConvertFrom(typeof(string)))
            {
                return false;
            }
            if (modelType == typeof(ComplexModelDto))
            {
                return false;
            }
            return true;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (!CanBindType(bindingContext.ModelType))
            {
                return false;
            }
            if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                return false;
            }
            //创建针对目标类型的空对象
            var ins = DynamicModelBuilder.GetInstance<IContact>(parent: bindingContext.ModelType, propAttrProvider: () =>
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

            //bindingContext.Model = Activator.CreateInstance(bindingContext.ModelType);
            bindingContext.Model = ins;

            //创建针对目标类型的ComplexModelDto
            //创建ModelBindingContext，并将ComplexModelDto作为Model，驱动新一轮的Model绑定
            ComplexModelDto dto = new ComplexModelDto(bindingContext.ModelMetadata, bindingContext.PropertyMetadata.Values);
            ModelBindingContext subContext = new ModelBindingContext(bindingContext)
            {
                ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(() => dto, typeof(ComplexModelDto)),
                ModelName = bindingContext.ModelName
            };
            actionContext.Bind(subContext);

            //从ComplexModelDto获取相应的值并为相应的属性赋值
            foreach (KeyValuePair<ModelMetadata, ComplexModelDtoResult> item in dto.Results)
            {
                ModelMetadata propertyMetadata = item.Key;
                ComplexModelDtoResult dtoResult = item.Value;
                if (dtoResult != null)
                {
                    PropertyInfo propertyInfo = bindingContext.ModelType.GetProperty(propertyMetadata.PropertyName);
                    if (propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(bindingContext.Model, dtoResult.Model);
                    }
                }
            }
            return true;
        }
    }
}
