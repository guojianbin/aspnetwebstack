﻿using System.Net.Http;
using System.Web.Http.Controllers;
using Xunit;
using Assert = Microsoft.TestCommon.AssertEx;

namespace System.Web.Http.Filters
{
    public class HttpActionExecutedContextTest
    {
        [Fact]
        public void Default_Constructor()
        {
            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext();

            Assert.Null(actionExecutedContext.ActionContext);
            Assert.Null(actionExecutedContext.Exception);
            Assert.Null(actionExecutedContext.Request);
            Assert.Null(actionExecutedContext.Result);
        }

        [Fact]
        public void Parameter_Constructor()
        {
            HttpActionContext context = ContextUtil.CreateActionContext();
            Exception exception = new Exception();

            var actionContext = new HttpActionExecutedContext(context, exception);

            Assert.Same(context, actionContext.ActionContext);
            Assert.Same(exception, actionContext.Exception);
            Assert.Same(context.ControllerContext.Request, actionContext.Request);
            Assert.Null(actionContext.Result);
        }

        [Fact]
        public void Constructor_AllowsNullExceptionParameter()
        {
            HttpActionContext context = ContextUtil.CreateActionContext();

            var actionContext = new HttpActionExecutedContext(context, exception: null);

            Assert.Null(actionContext.Exception);
        }

        [Fact]
        public void Constructor_IfContextParameterIsNull_ThrowsException()
        {
            Assert.ThrowsArgumentNull(() =>
            {
                new HttpActionExecutedContext(actionContext: null, exception: null);
            }, "actionContext");
        }

        [Fact]
        public void ActionContext_Property()
        {
            Assert.Reflection.Property<HttpActionExecutedContext, HttpActionContext>(
                instance: new HttpActionExecutedContext(),
                propertyGetter: aec => aec.ActionContext,
                expectedDefaultValue: null,
                allowNull: false,
                roundTripTestValue: ContextUtil.CreateActionContext());
        }

        [Fact]
        public void Exception_Property()
        {
            Assert.Reflection.Property<HttpActionExecutedContext, Exception>(
                instance: new HttpActionExecutedContext(),
                propertyGetter: aec => aec.Exception,
                expectedDefaultValue: null,
                allowNull: true,
                roundTripTestValue: new ArgumentException());
        }

        [Fact]
        public void Result_Property()
        {
            Assert.Reflection.Property<HttpActionExecutedContext, HttpResponseMessage>(
                instance: new HttpActionExecutedContext(),
                propertyGetter: aec => aec.Result,
                expectedDefaultValue: null,
                allowNull: true,
                roundTripTestValue: new HttpResponseMessage());
        }
    }
}
