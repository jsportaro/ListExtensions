using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ListExtensions.Tests
{

    public class EnumerableExtensionsTests
    {
        class StringModel
        {
            public string Name;
        }

        [Fact]
        public void Should_Create_String_Column()
        {
            var listOfStringModels = new List<StringModel>()
            {
            };

            var table = listOfStringModels.ToDataTable();

            Assert.True(table.Columns[0].DataType == typeof(string));
        }

        [Fact]
        public void Should_Create_New_Row_For_Each_Item_In_Enumerable()
        {
            var listOfStringModels = new List<StringModel>()
            {
                new StringModel() { Name = "Name" }
            };

            var table = listOfStringModels.ToDataTable();

            Assert.True(table.Rows.Count == listOfStringModels.Count);
        }

        [Fact]
        public void Should_Set_New_Row_Columns_To_Items_Field_Values()
        {
            var name = "Name";
            var listOfStringModels = new List<StringModel>()
            {
                new StringModel() { Name = name}
            };

            var table = listOfStringModels.ToDataTable();

            Assert.True((table.Rows[0]["Name"] as string) == name);
        }

        [Fact]
        public void Should_Return_Empty_Table_When_List_Is_Empty()
        {
            var listOfStringModels = new List<StringModel>()
            {
            };

            var table = listOfStringModels.ToDataTable();

            Assert.Empty(table.Rows);
        }

        class BigModel
        {
            public long Id;
            public string Name;
            public long OtherId;
            public string AnotherName;
            public long ReferencedId;
            public string ReferencedName;
        }

        [Fact]
        public void Stress_Test()
        {
            var list = new List<BigModel>();

            for (int i = 0; i < 10000; i++)
            {
                list.Add(new BigModel()
                {
                    Id = i,
                    Name = "Name",
                    OtherId = i + 1,
                    AnotherName = "AnotherName",
                    ReferencedId = i + 2,
                    ReferencedName = "ReferencedName"
                });
            }

            var table = list.ToDataTable();

            Assert.Equal(10000, table.Rows.Count);

        }
    }
}
