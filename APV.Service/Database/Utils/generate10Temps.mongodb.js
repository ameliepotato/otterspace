/* global use, db */
// MongoDB Playground
// Use Ctrl+Space inside a snippet or a string literal to trigger completions.

const database = 'Measurements';
const collection = 'Readings';

// The current database to use.
use(database);

if (db.Readings.exists()) {
}

var query = { SensorId: 'One' };
db.Readings.deleteMany(query);

var i = 0;

var sensor = "One";

for (; i < 10; i++) {
  query = { _id: i.toString() };
  db.Readings.deleteMany(query);
  var date = new Date(new Date().getTime() - ((i%3)+i) * 24 * 60 * 60 * 1000);
  db.Readings.insertOne(
    { _id: i.toString(), SensorId: sensor, Value: -10 + 2 * i, Time: date }
  );
}
