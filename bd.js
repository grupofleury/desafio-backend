
/* BANCO DE DADOS FIREBASE  */
require("firebase/auth");
require("firebase/database");

const admin = require('firebase-admin');

firebase = require('firebase');

var config = {
  apiKey: "AIzaSyAnMCgrfAi7jZvRDvMM0V_8kHzcL9bPFlw",
  authDomain: "grupofleury-2d28d.firebaseapp.com",
  databaseURL: "https://grupofleury-2d28d.firebaseio.com",
  projectId: "grupofleury-2d28d"
};


firebase.initializeApp(config);
bd = firebase.firestore();

