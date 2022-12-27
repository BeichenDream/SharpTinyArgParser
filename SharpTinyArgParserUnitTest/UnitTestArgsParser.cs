using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTinyArgParser;

namespace SharpTinyArgParserUnitTest
{
    [TestClass]
    public class UnitTestArgsParser
    {
        [TestMethod]
        public void TestParserBasicType()
        {
            string[] args = {"-name","mark","-age","20" };
            BasicType basicType = ArgsParser.ParseArgs<BasicType>(args);
            Assert.IsNotNull(basicType);
            Assert.IsTrue(basicType.Name == "mark");
            Assert.IsTrue(basicType.Age == 20);
        }
        [TestMethod]
        public void TestCheckRequired()
        {
            string[] args = { "-name", "mark" };
            bool isException = false;
            try
            {
                PrimitiveType basicType = ArgsParser.ParseArgs<PrimitiveType>(args);
            }
            catch (Exception)
            {
                isException = true;
            }
            Assert.IsTrue(isException);
        }
        [TestMethod]
        public void TestParserPrimitiveType()
        {
            string[] args = { "-name", "mark","-age","20", "-isMan","0", "-height","190"};
            PrimitiveType primitiveType = ArgsParser.ParseArgs<PrimitiveType>(args);
            Assert.IsTrue(primitiveType.Name == "mark");
            Assert.IsTrue(primitiveType.Age == 20);
            Assert.IsTrue(!primitiveType.IsMan);
            Assert.IsTrue(primitiveType.Height == 190);
        }
        [TestMethod]
        public void TestParserBoolType()
        {
            string[] args = { "-isMan", "0" };
            BoolType boolType = ArgsParser.ParseArgs<BoolType>(args);
            Assert.IsTrue(!boolType.IsMan);

            string[] args2 = { "-isMan", "1" };
            BoolType boolType2 = ArgsParser.ParseArgs<BoolType>(args2);
            Assert.IsTrue(boolType2.IsMan);

            string[] args3 = { "-isMan", "false" };
            BoolType boolType3 = ArgsParser.ParseArgs<BoolType>(args3);
            Assert.IsTrue(!boolType3.IsMan);

            string[] args4 = { "-isMan", "true" };
            BoolType boolType4 = ArgsParser.ParseArgs<BoolType>(args4);
            Assert.IsTrue(boolType4.IsMan);


            string[] args5 = { "-isMan", "123" };
            BoolType boolType5 = ArgsParser.ParseArgs<BoolType>(args5);
            Assert.IsTrue(boolType5.IsMan);

            string[] args6 = { "-isMan", "true" };
            BoolType boolType6 = ArgsParser.ParseArgs<BoolType>(args6);
            Assert.IsTrue(boolType6.IsMan);

        }
        [TestMethod]
        public void TestParserStringArrayType()
        {
            string[] args = { "-peopleName", "mark,jack" };
            StringArrayType stringArrayType = ArgsParser.ParseArgs<StringArrayType>(args);
            Assert.IsNotNull(stringArrayType);
            Assert.IsTrue(stringArrayType.PeopleNames[0] == "mark");
            Assert.IsTrue(stringArrayType.PeopleNames[1] == "jack");
        }
        [TestMethod]
        public void TestParserPrimitiveArrayType()
        {
            string[] args = { "-peopleAge", "1,2-5" };
            PrimitiveArrayType primitiveArrayType = ArgsParser.ParseArgs<PrimitiveArrayType>(args);
            Assert.IsNotNull(primitiveArrayType);
            Assert.IsTrue(primitiveArrayType.PeopleAges.Length == 5);
            Assert.IsTrue(primitiveArrayType.PeopleAges[0] == 1);
            Assert.IsTrue(primitiveArrayType.PeopleAges[1] == 2);
            Assert.IsTrue(primitiveArrayType.PeopleAges[2] == 3);
            Assert.IsTrue(primitiveArrayType.PeopleAges[3] == 4);
            Assert.IsTrue(primitiveArrayType.PeopleAges[4] == 5);
        }
        [TestMethod]
        public void TestGenerateHelpMessagebox()
        {
            string helpMsg = ArgsParser.PrintHelp(typeof(PrimitiveArrayType), "hello", "hello");
            Assert.IsTrue(helpMsg.Contains("hello -peopleAge 1"));
        }

        class BasicType
        {
            [ArgsParser.Args("name","jack")]
            public string Name { get; set; }
            [ArgsParser.Args("age", "18")]
            public short Age { get; set; }
            public string Other { get; set; }
        }
        class PrimitiveType
        {
            [ArgsParser.Args("name", "jack",Required = true)]
            public string Name { get; set; }
            [ArgsParser.Args("age", "18", Required = true)]
            public short Age { get; set; }
            [ArgsParser.Args("isMan", "1")]
            public bool IsMan { get; set; }
            [ArgsParser.Args("height", "180")]
            public long Height { get; set; }
        }
        class BoolType
        {
            [ArgsParser.Args("isMan", "false", Required = true)]
            public bool IsMan { get; set; }
        }
        class StringArrayType
        {
            [ArgsParser.Args("peopleName", "jack", Required = true)]
            public string[] PeopleNames { get; set; }
        }
        class PrimitiveArrayType
        {
            [ArgsParser.Args("peopleAge", "1", Required = true)]
            public int[] PeopleAges { get; set; }
        }
    }
}
