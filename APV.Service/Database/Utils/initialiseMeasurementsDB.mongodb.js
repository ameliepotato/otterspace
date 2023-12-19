/* global use, db */
// MongoDB Playground
// Use Ctrl+Space inside a snippet or a string literal to trigger completions.

const database = 'Measurements';
const collection = 'Readings';

// The current database to use.
use(database);

if(db.Readings.exists())
{
    db.Readings.drop();
}

// Create a new collection.
db.createCollection(collection);

db.Readings.insertOne(
    { _id:"1", SensorId: "One", Value: -10, Time: new Date() }
);
db.Readings.insertOne(
    { _id: "2", SensorId: "Two", Value: 30,  Time: new Date()}
);
db.Readings.insertOne(
    { _id: "4", SensorId: "Two", Value: 0,  Time: new Date("2022-10-03") }
);


db.Readings.insertOne(
    { _id: "5", SensorId: "Five", Value: 22,  Time: new Date("2022-10-05") }
);
