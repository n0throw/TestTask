# TestTask Ver 1.0

This is the first version of the project. Crutches were made to separate the streams into packets:

1. RequestQueue class in the server side.
2. RequestSender class in the client side.

* Request Queue - provides a restriction of processed packets
* Request Sender - emulates sending packets by the client. When sending data, a new client is created.
