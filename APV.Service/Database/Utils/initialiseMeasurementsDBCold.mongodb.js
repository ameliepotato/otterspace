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
    { _id: "2", SensorId: "Two", Value: -30,  Time: new Date()}

);
db.Readings.insertOne(
    { _id: "5", SensorId: "Five", Value: -22,  Time: new Date() }
);

db.Readings.insertOne(
    { _id: "7", SensorId: "Six", Value: 23,  Time: new Date() }
);
db.Readings.insertOne(
    { _id: "8", SensorId: "Seven", Value: -25,  Time: new Date() }
);


db.Readings.insertOne(
    { _id: "10", SensorId: "Eight", Value: 35,  Time: new Date() }
);


// More information on the `createCollection` command can be found at:
// https://www.mongodb.com/docs/manual/reference/method/db.createCollection/
