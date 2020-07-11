# Paycom-PubSubBroker

## About
Publisher/Subscriber/Broker project for Paycom's summer engagment program. <br>
Project is developed in .NET using TCP sockets <br>
Publishers can create topics and post messages to them, as well as delete them. <br>
Subscribers are able to query available topics and subscribe to those topics. They will receive all messages posted to those topics after subscribing. <br>
The server will disconnect closed clients by polling them every 30 seconds.

## How to use
Publisher/Subscriber/Broker are all included in the PubSubBroker.sln. Publish the three projects and run them individually. <br>
There are no limits to the amount of publishers/subscribers, and not limit on the amount of topics that can be created. <br>

Type "Help" on either the publisher or subscriber to get the list of commands. Commands are not case sensitive. <br>

### Commands
<b> Publisher: </b> Compose Message, Query Topics, Create Topic, Delete Topic <br>
<b> Subscriber: </b> Subscribe, Unsubscribe, Query Topics <br>

