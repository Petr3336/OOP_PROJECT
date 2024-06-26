// axios-config.js

import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "", // Замените на ваш базовый URL
  headers: {
    "Content-Type": "application/json",
  },
});

axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem("accessToken"); // Замените на ваш метод хранения токена
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

let retryCount = 0; // Счетчик попыток

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response.status === 401) {
      if (localStorage.getItem("refreshToken") && retryCount < 3) {
        retryCount++;
        // Обновляем токен
        return axiosInstance
          .post("/api/refresh", {
            refreshToken: localStorage.getItem("refreshToken"),
          })
          .then((response) => {
            retryCount = 0;
            const newToken = response.data.accessToken; // Замените на ваш метод получения нового токена
            const newRefresh = response.data.refreshToken;
            localStorage.setItem("accessToken", newToken);
            localStorage.setItem("refreshToken", newRefresh);
            console.log(error.config);
            return axiosInstance.request(error.config); // Перезапускаем запрос с новым токеном
          })
          .catch((error) => {
            console.log("err");
            localStorage.removeItem("refreshToken");
            localStorage.removeItem("accessToken");
            throw error;
          });
      } else {
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("accessToken");
      }
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
