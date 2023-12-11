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
    { _id:"1", SensorId: "One", Value: 33, Time: new Date("2021-09-05") }
);
db.Readings.insertOne(
    { _id: "2", SensorId: "Two", Value: 32,  Time: new Date ("2022-03-05")}
);
db.Readings.insertOne(
    { _id: "4", SensorId: "Two", Value: 24,  Time: new Date("2023-10-05") }
);


// More information on the `createCollection` command can be found at:
// https://www.mongodb.com/docs/manual/reference/method/db.createCollection/
