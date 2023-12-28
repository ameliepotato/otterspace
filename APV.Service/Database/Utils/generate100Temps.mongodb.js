/* global use, db */
// MongoDB Playground
// Use Ctrl+Space inside a snippet or a string literal to trigger completions.

const database = 'Measurements';
const collection = 'Readings';

// The current database to use.
use(database);

var query = { SensorId: 'Two' };
db.Readings.deleteMany(query);

var i = 0;

var sensor = "Two";

for (; i < 100; i++) {
  query = { _id: (i + 100).toString() };
  db.Readings.deleteMany(query);
  var date = new Date(new Date().getTime() - i * 24 * 60 * 60 * 1000);
  db.Readings.insertOne({ 
      _id: (i + 100).toString(), 
      SensorId: sensor, 
      Value: parseInt(Math.random()*80-40), 
      Time: date }
  );
}
