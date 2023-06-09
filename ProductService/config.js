import dotenv from 'dotenv';
dotenv.config();

const mongoUrl = process.env.MONGO_URL;
const testServerUrl = process.env.TEST_SERVER_URL;

export const config = {
    dbUrl: mongoUrl,
    options: {
        useNewUrlParser: true,
        useUnifiedTopology: true,
    },
    testServer: testServerUrl
};