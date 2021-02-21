﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static OperationResult.Helpers;
using static OperationResult.Tests.Helpers;

namespace OperationResult.Tests
{
    [TestClass]
    public class ResultTests
    {
        private Result<int> GetResult(int arg)
        {
            if (arg == 1)
            {
                return arg;
            }
            if (arg == 2)
            {
                return Ok(arg);
            }
            return Error();
        }

        [TestMethod]
        public void TestResultWithoutError()
        {
            var res1 = GetResult(1);

            Assert.IsTrue(res1);
            Assert.IsTrue(res1.IsSuccess);
            Assert.IsFalse(res1.IsError);
            Assert.AreEqual(res1.Value, 1);

            var res2 = GetResult(2);

            Assert.IsTrue(res2);
            Assert.IsTrue(res2.IsSuccess);
            Assert.IsFalse(res2.IsError);
            Assert.AreEqual(res2.Value, 2);

            var res3 = GetResult(3);

            Assert.IsFalse(res3);
            Assert.IsFalse(res3.IsSuccess);
            Assert.IsTrue(res3.IsError);
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res3.Value) );
        }

        [TestMethod]
        public void TestResultWithoutErrorImplicitToBool()
        {
            bool isSuccess;

            isSuccess = GetResult(1);
            Assert.IsTrue(isSuccess);

            isSuccess = GetResult(2);
            Assert.IsTrue(isSuccess);

            isSuccess = GetResult(3);
            Assert.IsFalse(isSuccess);
        }

        private Result<int, string> GetResultOrError(int arg)
        {
            if (arg == 1)
            {
                return arg;
            }
            if (arg == 2)
            {
                return Ok(arg);
            }
            return Error("Invalid Operation");
        }

        [TestMethod]
        public void TestResultWithError()
        {
            var res1 = GetResultOrError(1);

            Assert.IsTrue(res1);
            Assert.IsTrue(res1.IsSuccess);
            Assert.IsFalse(res1.IsError);
            Assert.AreEqual(res1.Value, 1);
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res1.Error));

            var res2 = GetResultOrError(2);

            Assert.IsTrue(res2);
            Assert.IsTrue(res2.IsSuccess);
            Assert.IsFalse(res2.IsError);
            Assert.AreEqual(res2.Value, 2);
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res2.Error));

            var res3 = GetResultOrError(3);

            Assert.IsFalse(res3);
            Assert.IsFalse(res3.IsSuccess);
            Assert.IsTrue(res3.IsError);
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res3.Value));
            Assert.AreEqual(res3.Error, "Invalid Operation");
        }

        [TestMethod]
        public void TestResultWithErrorImplicitToBool()
        {
            bool isSuccess;

            isSuccess = GetResultOrError(1);
            Assert.IsTrue(isSuccess);

            isSuccess = GetResultOrError(2);
            Assert.IsTrue(isSuccess);

            isSuccess = GetResultOrError(3);
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void TestResultWithErrorDeconstruction()
        {
            int result;
            string error;

            (result, error) = GetResultOrError(1);
            Assert.AreEqual(result, 1);
            Assert.IsNull(error);

            (result, error) = GetResultOrError(2);
            Assert.AreEqual(result, 2);
            Assert.IsNull(error);

            (result, error) = GetResultOrError(3);
            Assert.AreEqual(result, default(int));
            Assert.AreEqual(error, "Invalid Operation");
        }

        private Result<int, string, int> GetResultOrMultipleErrors(int arg)
        {
            if (arg == 1)
            {
                return arg;
            }
            if (arg == 2)
            {
                return Ok(arg);
            }
            if (arg == 3)
            {
                return Error(404);
            }
            return Error("Invalid Operation");
        }

        [TestMethod]
        public void TestResultWithMultipleErrors()
        {
            var res1 = GetResultOrMultipleErrors(1);

            Assert.IsTrue(res1);
            Assert.IsTrue(res1.IsSuccess);
            Assert.IsFalse(res1.IsError);
            Assert.AreEqual(res1.Value, 1);
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res1.Error));

            var res2 = GetResultOrMultipleErrors(2);

            Assert.IsTrue(res2);
            Assert.IsTrue(res2.IsSuccess);
            Assert.IsFalse(res2.IsError);
            Assert.AreEqual(res2.Value, 2);
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res2.Error));

            var res3 = GetResultOrMultipleErrors(3);

            Assert.IsFalse(res3);
            Assert.IsFalse(res3.IsSuccess);
            Assert.IsTrue(res3.IsError);
            Assert.IsTrue(res3.HasError<int>());
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res3.Value));
            Assert.AreEqual(res3.Error, 404);
            Assert.AreEqual(res3.GetError<int>(), 404);

            var res4 = GetResultOrMultipleErrors(4);

            Assert.IsFalse(res4);
            Assert.IsFalse(res4.IsSuccess);
            Assert.IsTrue(res4.IsError);
            Assert.IsTrue(res4.HasError<string>());
            Assert.ThrowsException<NullReferenceException>(() => DoNothing(res4.Value));
            Assert.AreEqual(res4.Error, "Invalid Operation");
            Assert.AreEqual(res4.GetError<string>(), "Invalid Operation");
        }

        [TestMethod]
        public void TestResultWithMultipleErrorsImplicitToBool()
        {
            bool isSuccess;

            isSuccess = GetResultOrMultipleErrors(1);
            Assert.IsTrue(isSuccess);

            isSuccess = GetResultOrMultipleErrors(2);
            Assert.IsTrue(isSuccess);

            isSuccess = GetResultOrMultipleErrors(3);
            Assert.IsFalse(isSuccess);

            isSuccess = GetResultOrMultipleErrors(4);
            Assert.IsFalse(isSuccess);
        }
    }
}
