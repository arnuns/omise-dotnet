﻿using System;
using NUnit.Framework;

namespace Omise.Net.NUnit.Test
{
	[TestFixture]
	public class CustomerTest:TestBase
	{
		[Test]
		public void TestCreateCustomer(){
			var customerInfo = new CustomerInfo ();
			customerInfo.Email = "test2@localhost";
			customerInfo.Description = "Test customer 2";

			StubRequestWithResponse (@"{
								    'object': 'customer',
								    'id': '123',
								    'livemode': false,
								    'location': '/customers/123',
								    'default_card': null,
								    'email': 'test2@localhost',
								    'description': null,
								    'created': '2014-10-02T08:12:12Z',
								    'cards': {
								        'object': 'list',
								        'from': '1970-01-01T07:00:00+07:00',
								        'to': '2014-10-02T15:12:12+07:00',
								        'offset': 0,
								        'limit': 20,
								        'total': 0,
								        'data': [],
								        'location': '/customers/123/cards'
								    }
								}");

			var customerResult = client.CustomerService.CreateCustomer (customerInfo);
			Assert.IsNotNull (customerResult);
			Assert.AreEqual ("123", customerResult.Id);
			Assert.AreEqual ("test2@localhost", customerResult.Email);
			Assert.IsFalse (customerResult.LiveMode);
			Assert.AreEqual ("/customers/123", customerResult.Location);
			Assert.IsNullOrEmpty (customerResult.DefaultCardId);
			Assert.IsNullOrEmpty (customerResult.Description);
			Assert.AreEqual (new DateTime (2014, 10, 2, 8, 12, 12), customerResult.CreatedAt);
			Assert.IsNotNull (customerResult.CardCollection);
			Assert.AreEqual (new DateTime (1970, 1, 1, 7, 0, 0), customerResult.CardCollection.From);
			Assert.AreEqual (new DateTime (2014, 10, 2, 15, 12, 12), customerResult.CardCollection.To);
			Assert.AreEqual (0, customerResult.CardCollection.Offset);
			Assert.AreEqual (20, customerResult.CardCollection.Limit);
			Assert.AreEqual (0, customerResult.CardCollection.Total);
			Assert.AreEqual (0, customerResult.CardCollection.Collection.Count);
			Assert.AreEqual ("/customers/123/cards", customerResult.CardCollection.Location);
		}

		[Test]
		public void TestUpdateCustomer(){
			var customerUpdateInfo = new CustomerInfo ();
			customerUpdateInfo.Email = "test11@localhost";
			customerUpdateInfo.Description = "Test Customer 11 change email";
			StubRequestWithResponse (@"{
								    'object': 'customer',
								    'id': '123',
								    'livemode': false,
								    'location': '/customers/123',
								    'default_card': null,
								    'email': 'test11@localhost',
								    'description': 'Test Customer 11 change email',
								    'created': '2014-10-02T08:12:12Z',
								    'cards': {
								        'object': 'list',
								        'from': '1970-01-01T07:00:00+07:00',
								        'to': '2014-10-02T15:31:35+07:00',
								        'offset': 0,
								        'limit': 20,
								        'total': 0,
								        'data': [],
								        'location': '/customers/123/cards'
								    }
								}");
			var customerUpdateResult = client.CustomerService.UpdateCustomer ("123", customerUpdateInfo);
			Assert.IsNotNull (customerUpdateResult);
			Assert.AreEqual ("123", customerUpdateResult.Id);
			Assert.AreEqual ("test11@localhost", customerUpdateResult.Email);
			Assert.IsFalse (customerUpdateResult.LiveMode);
			Assert.AreEqual ("/customers/123", customerUpdateResult.Location);
			Assert.IsNullOrEmpty (customerUpdateResult.DefaultCardId);
			Assert.AreEqual ("Test Customer 11 change email", customerUpdateResult.Description);
			Assert.AreEqual (new DateTime (2014, 10, 2, 8, 12, 12), customerUpdateResult.CreatedAt);
			Assert.IsNotNull (customerUpdateResult.CardCollection);
			Assert.AreEqual (new DateTime (1970, 1, 1, 7, 0, 0), customerUpdateResult.CardCollection.From);
			Assert.AreEqual (new DateTime (2014, 10, 2, 15, 31, 35), customerUpdateResult.CardCollection.To);
			Assert.AreEqual (0, customerUpdateResult.CardCollection.Offset);
			Assert.AreEqual (20, customerUpdateResult.CardCollection.Limit);
			Assert.AreEqual (0, customerUpdateResult.CardCollection.Total);
			Assert.AreEqual (0, customerUpdateResult.CardCollection.Collection.Count);
			Assert.AreEqual ("/customers/123/cards", customerUpdateResult.CardCollection.Location);
		}

		[Test]
		public void TestDeleteCustomer(){
			var json = @"{
    					'object': 'customer',
					    'id': '123',
					    'livemode': false,
					    'deleted': true
						}";

			StubRequestWithResponse (json);

			var result = client.CustomerService.DeleteCustomer ("123");
			Assert.AreEqual ("123", result.Id);
			Assert.IsTrue (result.Deleted);
			StubExceptionThrow (new ApiException ());
			Assert.Throws<ApiException> (delegate {
				client.CustomerService.GetCustomer ("123");
			});
		}

		[Test]
		public void TestGetCustomerInfo(){
			StubRequestWithResponse (@"{
								    'object': 'customer',
								    'id': '123',
								    'livemode': false,
								    'location': '/customers/123',
								    'default_card': null,
								    'email': 'test2@localhost',
								    'description': 'Test customer 1',
								    'created': '2014-10-02T08:12:12Z',
								    'cards': {
								        'object': 'list',
								        'from': '1970-01-01T07:00:00+07:00',
								        'to': '2014-10-02T15:28:20+07:00',
								        'offset': 0,
								        'limit': 20,
								        'total': 0,
								        'data': [],
								        'location': '/customers/123/cards'
								    }
								}");
			var customerShowResult = client.CustomerService.GetCustomer ("123");
			Assert.IsNotNull (customerShowResult);
			Assert.AreEqual ("123", customerShowResult.Id);
			Assert.AreEqual ("test2@localhost", customerShowResult.Email);
			Assert.IsFalse (customerShowResult.LiveMode);
			Assert.AreEqual ("/customers/123", customerShowResult.Location);
			Assert.IsNullOrEmpty (customerShowResult.DefaultCardId);
			Assert.AreEqual ("Test customer 1", customerShowResult.Description);
			Assert.AreEqual (new DateTime (2014, 10, 2, 8, 12, 12), customerShowResult.CreatedAt);
			Assert.IsNotNull (customerShowResult.CardCollection);
			Assert.AreEqual (new DateTime (1970, 1, 1, 7, 0, 0), customerShowResult.CardCollection.From);
			Assert.AreEqual (new DateTime (2014, 10, 2, 15, 28, 20), customerShowResult.CardCollection.To);
			Assert.AreEqual (0, customerShowResult.CardCollection.Offset);
			Assert.AreEqual (20, customerShowResult.CardCollection.Limit);
			Assert.AreEqual (0, customerShowResult.CardCollection.Total);
			Assert.AreEqual (0, customerShowResult.CardCollection.Collection.Count);
			Assert.AreEqual ("/customers/123/cards", customerShowResult.CardCollection.Location);
		}
	}
}

