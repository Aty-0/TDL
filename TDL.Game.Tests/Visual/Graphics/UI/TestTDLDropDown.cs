
using System;
using System.Linq;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Framework.Localisation;
using osu.Framework.Testing;
using osuTK;
using osuTK.Input;

using TDL.Game.Graphics.UI;

namespace TDL.Game.Tests.Visual.Graphics.UI
{
    [TestFixture]
    public partial class TestTDLDropDown : TDLTestScene
    {
        private const int items_to_add = 10;
        private const float explicit_height = 100;
        private float calculatedHeight;
        private TestDropdown testDropdown;
        private readonly PlatformActionContainer platformActionContainerKeyboardSelection, platformActionContainerKeyboardPreselection, platformActionContainerEmptyDropdown;
        private readonly BindableList<TestModel> bindableList = new BindableList<TestModel>();

        private int previousIndex;
        private int lastVisibleIndexOnTheCurrentPage, lastVisibleIndexOnTheNextPage;
        private int firstVisibleIndexOnTheCurrentPage, firstVisibleIndexOnThePreviousPage;

        public TestTDLDropDown()
        {


        }
        [SetUpSteps]
        public void T()
        {
            var testItems = new TestModel[10];
            int i = 0;
            while (i < items_to_add)
                testItems[i] = @"test " + i++;

            Children = new Drawable[]
            {
                 testDropdown = new TestDropdown
                 {
                     Width = 150,
                     Position = new Vector2(50, 50),
                     Items = testItems
                 },
            };

        }

        [Test]
        public void TestBasic()
        {

            int i = items_to_add;

            AddRepeatStep("add item", () => testDropdown.AddDropdownItem("test " + i++), items_to_add);
            AddAssert("item count is correct", () => testDropdown.Items.Count() == items_to_add * 2);
     
        }


        private class TestModel : IEquatable<TestModel>
        {
            public readonly string Identifier;

            public TestModel(string identifier)
            {
                Identifier = identifier;
            }

            public bool Equals(TestModel other)
            {
                if (other == null)
                    return false;

                return other.Identifier == Identifier;
            }

            public override string ToString() => Identifier;

            public static implicit operator TestModel(string str) => new TestModel(str);
        }

        private partial class TestDropdown : TDLDropDown<TestModel>
        {
            internal new DropdownMenuItem<TestModel> SelectedItem => base.SelectedItem;
        }

        /// <summary>
        /// Dropdown that will access state set by BDL load in <see cref="GenerateItemText"/>.
        /// </summary>
        private partial class BdlDropdown : TestDropdown
        {
            private string text;

            [BackgroundDependencyLoader]
            private void load()
            {
                text = "loaded";
            }

            protected override LocalisableString GenerateItemText(TestModel item)
            {
                Assert.That(text, Is.Not.Null);
                return $"{text}: {base.GenerateItemText(item)}";
            }
        }
    }
}


