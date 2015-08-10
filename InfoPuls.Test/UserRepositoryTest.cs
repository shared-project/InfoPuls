using System;
using System.Collections.Generic;
using InfoPuls.Model.Entity;
using InfoPuls.Model.DataAccess;
using NUnit.Framework;

namespace InfoPuls.Model.Test
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private Dictionary<string, User> _first;
        private Dictionary<string, User> _second;
        private IRepository<User> _repository;

        [SetUp]
        public void Init()
        {
            _first = new Dictionary<string, User>(StringComparer.OrdinalIgnoreCase);
            _second = new Dictionary<string, User>(StringComparer.OrdinalIgnoreCase);
            _repository = new UserRepository();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSimpleIntersection__NullArguments__ArgumentNullException()
        {
            _repository.GetSimpleIntersection(null, null);
        }

        [Test]
        public void GetSimpleIntersection__2Users_0SameKey__0User()
        {
            // arrange
            User user1 = new UserBuilder().Build();
            User user2 = new UserBuilder().WithEmail("2_example@mail.com").Build();

            _first.Add(user1.Email, user1);
            _second.Add(user2.Email, user2);

            // act
            var result = _repository.GetSimpleIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void GetSimpleIntersection__3Users_2SameKey_3SameUser__1User()
        {
            // arrange
            User user1 = new UserBuilder().WithEmail("1_example@mail.com").Build();
            User user2 = new UserBuilder().WithEmail("2_example@mail.com").Build();

            _first.Add(user1.Email, user1); // key1 user1

            _second.Add(user1.Email, user1); // key1 user1
            _second.Add(user2.Email, user1);

            // act
            var result = _repository.GetSimpleIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 1);
            Assert.That(result[user1.Email].Email, Is.EqualTo(user1.Email));
        }

        [Test]
        public void GetSimpleIntersection__2Users_2CaseSensetiveKey_2SameUser__1User()
        {
            // arrange
            User user1 = new UserBuilder().WithEmail("1_example@mail.com").Build();

            _first.Add(user1.Email, user1); // key1 user1
            _second.Add(user1.Email.ToUpper(), user1);// key1.ToUpper user1 

            // act
            var result = _repository.GetSimpleIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 1);
            Assert.That(result[user1.Email].Email, Is.EqualTo(user1.Email));
        }

        [Test]
        public void GetSimpleIntersection__5Users_2SameKey_IgnoreUser__1User()
        {
            // arrange
            User user1 = new UserBuilder().WithEmail("1_example@mail.com").Build();
            User user2 = new UserBuilder().WithEmail("2_example@mail.com").Build();
            User user3 = new UserBuilder().WithEmail("3_example@mail.com").Build();
            User user4 = new UserBuilder().WithEmail("4_example@mail.com").Build();

            _first.Add(user1.Email, user4); // key1 user4
            _first.Add(user2.Email, user4);

            _second.Add(user1.Email, user1); // key1 user1
            _second.Add(user3.Email, user1);
            _second.Add(user4.Email, user1);

            // act
            var result = _repository.GetSimpleIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 1);
            Assert.That(result[user1.Email].Email, Is.EqualTo(user4.Email));
        }

        [Test]
        public void GetSimpleIntersection__5Users_2SameKey_GetUserFromMinLengthDictionary__1User()
        {
            // arrange
            User user1 = new UserBuilder().WithEmail("1_example@mail.com").Build();
            User user2 = new UserBuilder().WithEmail("2_example@mail.com").Build();
            User user3 = new UserBuilder().WithEmail("3_example@mail.com").Build();
            User user4 = new UserBuilder().WithEmail("4_example@mail.com").Build();

            _first.Add(user1.Email, user4); // key1 user4
            _first.Add(user2.Email, user4);

            _second.Add(user1.Email, user1); // key1 user1
            _second.Add(user3.Email, user1);
            _second.Add(user4.Email, user1);

            // act
            var result = _repository.GetSimpleIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 1);
            Assert.That(result[user1.Email].Email, Is.EqualTo(user4.Email));
            Assert.That(result[user1.Email].Email, Is.Not.EqualTo(user1.Email));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetStrongIntersection__NullArguments__ArgumentNullException()
        {
            _repository.GetStrongIntersection(null, null);
        }

        [Test]
        public void GetStrongIntersection__2Users_2SameKey_2SameUser__1User()
        {
            // arrange
            User user1 = new UserBuilder().Build();
            User user2 = new UserBuilder().Build();

            _first.Add(user1.Email, user1);
            _second.Add(user2.Email, user2);

            // act
            var result = _repository.GetStrongIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 1);
            Assert.That(result[user1.Email].Email, Is.EqualTo(user1.Email));
        }

        [Test]
        public void GetStrongIntersection__2Users_2CaseSensetiveKey_2SameUser__1User()
        {
            // arrange
            User user1 = new UserBuilder().Build();
            User user2 = new UserBuilder().Build();

            _first.Add(user1.Email, user1);
            _second.Add(user2.Email.ToUpper(), user2); // key.ToUpper

            // act
            var result = _repository.GetStrongIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 1);
            Assert.That(result[user1.Email].Email, Is.EqualTo(user1.Email));
        }

        [Test]
        public void GetStrongIntersection__2Users_0SameKey__0User()
        {
            // arrange
            User user1 = new UserBuilder().Build();
            User user2 = new UserBuilder().WithEmail("2_some@mail.com").Build();

            _first.Add(user1.Email, user1);
            _second.Add(user2.Email, user2);

            // act
            var result = _repository.GetStrongIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void GetStrongIntersection__2Users_2SameKey_0SameUserName__0User()
        {
            // arrange
            User user1 = new UserBuilder().Build();
            User user2 = new UserBuilder().WithFirstName("John").Build();

            _first.Add(user1.Email, user1);
            _second.Add(user2.Email, user2);

            // act
            var result = _repository.GetStrongIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void GetStrongIntersection__2Users_2SameKey_0SameUserLastName__0User()
        {
            // arrange
            User user1 = new UserBuilder().Build();
            User user2 = new UserBuilder().WithLastName("Doe").Build();

            _first.Add(user1.Email, user1);
            _second.Add(user2.Email, user2);

            // act
            var result = _repository.GetStrongIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void GetStrongIntersection__2Users_2SameKey_0SameUserBirthDate__0User()
        {
            // arrange
            User user1 = new UserBuilder().Build();
            User user2 = new UserBuilder().WithBirthDate("1/4/1964").Build();

            _first.Add(user1.Email, user1);
            _second.Add(user2.Email, user2);

            // act
            var result = _repository.GetStrongIntersection(_first, _second);

            // assert
            Assert.AreEqual(result.Count, 0);
        }
    }
}
