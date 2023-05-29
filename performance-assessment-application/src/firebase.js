// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyAOVXrrv4E8-iOL-VpvNknCAR9VpJarxzs",
  authDomain: "performance-assessment-c9485.firebaseapp.com",
  projectId: "performance-assessment-c9485",
  storageBucket: "performance-assessment-c9485.appspot.com",
  messagingSenderId: "304338500801",
  appId: "1:304338500801:web:a42c3d48a3d68a850fe840",
  measurementId: "G-TZ08H4CPFJ"
};

// Initialize Firebase
export const app = initializeApp(firebaseConfig);