import axios from "axios";

export const axiosInstance = axios.create({
    baseURL:"https:localhost:7198",
    headers:{
        "Content-Type":"application/json"
    }
});