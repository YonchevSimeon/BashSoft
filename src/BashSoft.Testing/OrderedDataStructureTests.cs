namespace BashSoft.Testing
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    using NUnit.Framework;

    using BashSoft.Contracts;
    using BashSoft.DataStructures;

    [TestFixture]
    public class OrderedDataStructureTests
    {
        private ISimpleOrderedBag<string> sut;

        [SetUp]
        public void SetUp()
        {
            this.sut = new SimpleSortedList<string>();
        }

        [Test]
        public void TestEmptyCtor()
        {
            this.sut = new SimpleSortedList<string>();
            Assert.AreEqual(this.sut.Capacity, 16);
            Assert.AreEqual(this.sut.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            this.sut = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.sut.Capacity, 20);
            Assert.AreEqual(this.sut.Size, 0);
        }

        [Test]
        public void TestCtorWithAllParams()
        {
            this.sut = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);
            Assert.AreEqual(this.sut.Capacity, 30);
            Assert.AreEqual(this.sut.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialComparer()
        {
            this.sut = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.sut.Capacity, 16);
            Assert.AreEqual(this.sut.Size, 0);
        }

        [Test]
        public void TestAddIncreasesSize()
        {
            this.sut.Add("Name");
            Assert.AreEqual(this.sut.Size, 1);
        }

        [Test]
        public void TestAddNullException()
        {
            Assert.That(() => this.sut.Add(null),
                Throws.ArgumentNullException.With.Message.EqualTo(
                    "Value cannot be null."));
        }

        [Test]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            this.sut = new SimpleSortedList<string>() { "Rosen", "Georgi", "Balkan" };

            string[] sorted = new string[] { "Balkan", "Georgi", "Rosen" };

            Assert.AreEqual(sorted, this.sut);
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            for (int i = 0; i < 17; i++)
            {
                this.sut.Add("Name");
            }

            Assert.AreEqual(this.sut.Size, 17);
            Assert.That(this.sut.Capacity, !Is.EqualTo(16));
        }

        [Test]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            IList<string> listToAdd = new List<string>() { "Name", "Name2" };

            this.sut.AddAll(listToAdd);

            Assert.AreEqual(this.sut.Size, 2);
            Assert.AreEqual(this.sut.Capacity, 16);
        }

        [Test]
        public void TestAddingAllFromNullThrowsException()
        {
            IList<string> listToAdd = new List<string>() { "Name", null };

            Assert.That(() => this.sut.AddAll(listToAdd),
                Throws.ArgumentNullException.With.Message.EqualTo(
                    "Value cannot be null."));
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            string[] collectionToAdd = new string[] { "Rosen", "Georgi", "Balkan" };

            string[] sorted = new string[] { "Balkan", "Georgi", "Rosen" };

            this.sut.AddAll(collectionToAdd);

            Assert.AreEqual(sorted, this.sut);
        }

        [Test]
        public void TestRemoveValidElementDecreaseSize()
        {
            string element = "Name";

            this.sut.Add(element);

            Assert.AreEqual(this.sut.Size, 1);

            this.sut.Remove(element);

            Assert.AreEqual(this.sut.Size, 0);
        }

        [Test]
        public void TetsRemoveValidElementRemovesSelectedOne()
        {
            string elementIvan = "ivan";
            string elementNasko = "nasko";

            this.sut.Add(elementIvan);
            this.sut.Add(elementNasko);

            this.sut.Remove(elementIvan);

            Assert.IsFalse(this.sut.Any(e => e == elementIvan));
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            Assert.That(() => this.sut.Remove(null),
                Throws.ArgumentNullException.With.Message.EqualTo(
                    "Value cannot be null."));
        }

        [Test]
        public void TetsJoinWithNull()
        {
            Assert.That(() => this.sut.JoinWith(null),
                   Throws.ArgumentNullException.With.Message.EqualTo(
                       "Value cannot be null."));
        }

        [Test]
        public void TestJoinWithWorksFine()
        {
            this.sut.AddAll(new string[] { "Name1", "Name2" });

            Assert.AreEqual("Name1, Name2", this.sut.JoinWith(", "));
        }
    }
}
