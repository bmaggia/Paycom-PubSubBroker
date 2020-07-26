using Microsoft.VisualStudio.TestTools.UnitTesting;

using PubSubBroker;
using System.Net.Sockets;
using PubSubBroker.Commands;

namespace BrokerTests
{
    [TestClass]
    public class BrokerTesting
    {
        [TestMethod]
        public void AddMessageCreateTopic()
        {
            Messages.BrokerMessages.Clear();

            Message expected = new Message("Test Topic", "Test Message");

            Messages.AddMessage("Test Topic", "Test Message");

            Message actual = Messages.BrokerMessages[0];

            Assert.AreEqual(expected.Topic, actual.Topic, "Topic is not set correctly");
            Assert.AreEqual(expected.TopicMessages[0], actual.TopicMessages[0], "Message was not added to topic");
        }

        [TestMethod]
        public void RemoveTopic()
        {
            Messages.BrokerMessages.Clear();

            Message expected = new Message("Test Topic", "Test Message");

            Messages.AddMessage("Test Topic", "Test Message");

            Messages.DeleteTopic("Test Topic");

            Assert.AreEqual(Messages.BrokerMessages.Count, 0, "Topic was not removed correctly");
        }

        [TestMethod]
        public void CreateTopic()
        {
            Messages.BrokerMessages.Clear();

            Messages.CreateTopic("Test Topic");

            Message actual = Messages.BrokerMessages[0];

            Assert.AreEqual("Test Topic", actual.Topic, "Topic is not set correctly");
            Assert.AreEqual(Messages.BrokerMessages.Count, 1, "Message was added, only a topic should be created");
        }

        [TestMethod]
        public void AddMessageToExistingTopic()
        {
            Messages.BrokerMessages.Clear();

            Message expected = new Message("Test Topic", "Test Message 2");

            Messages.AddMessage("Test Topic", "Test Message");

            Messages.AddMessage("Test Topic", "Test Message 2");

            Message actual = Messages.BrokerMessages[0];

            Assert.AreEqual(expected.Topic, actual.Topic, "Topic is not set correctly");

            Assert.AreEqual(expected.TopicMessages[0], actual.TopicMessages[1], "Message was not added to topic");
        }

        [TestMethod]
        public void AddSubscriber()
        {
            Messages.BrokerMessages.Clear();

            Messages.AddMessage("Test Topic", "Test Message");

            NetworkStream netStream = null;

            string actual = Messages.AddSubscriber("Test Topic", netStream);
            string expected = "Successfully subscribed to: Test Topic";

            Assert.AreEqual(actual, expected, "Subscriber not added correctly");
        }

        [TestMethod]
        public void RemoveSubscriber()
        {
            Messages.BrokerMessages.Clear();

            Messages.AddMessage("Test Topic", "Test Message");

            NetworkStream netStream = null;

            Messages.AddSubscriber("Test Topic", netStream);
            string actual = Messages.RemoveSubscriber("Test Topic", netStream);

            string expected = "Successfully unsubscribed to: Test Topic";

            Assert.AreEqual(actual, expected, "Subscriber not removed correctly");
        }

        [TestMethod]
        public void AddRemoveFromNonExistentTopic()
        {
            Messages.BrokerMessages.Clear();

            NetworkStream netStream = null;

            string actual = Messages.AddSubscriber("Test Topic", netStream);

            string expected = "Test Topic does not exist";

            Assert.AreEqual(actual, expected, "Subscribing from nonexistent topic not working");

            actual = Messages.RemoveSubscriber("Test Topic", netStream);

            Assert.AreEqual(actual, expected, "Unsubscribing from nonexistent topic not working");
        }

        [TestMethod]
        public void RemoveSubscriberWhenNotSubscribed()
        {
            Messages.BrokerMessages.Clear();

            Messages.AddMessage("Test Topic", "Test Message");

            NetworkStream netStream = null;

            string actual = Messages.RemoveSubscriber("Test Topic", netStream);

            string expected = "Not subscribed to: Test Topic";

            Assert.AreEqual(actual, expected, "Subscriber not removed correctly");
        }

        [TestMethod]
        public void AddSubscriberWhenAlreadySubscribed()
        {
            Messages.BrokerMessages.Clear();

            Messages.AddMessage("Test Topic", "Test Message");

            NetworkStream netStream = null;

            Messages.AddSubscriber("Test Topic", netStream);

            string actual = Messages.AddSubscriber("Test Topic", netStream);

            string expected = "Already subscribed to: Test Topic";

            Assert.AreEqual(actual, expected, "Subscriber not removed correctly");
        }
    }
}
