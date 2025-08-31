# 🌞 Solarness

**Solarness** is a solar project management application developed using modern technologies to ensure **scalability, efficiency, and system reliability**.  
The application enables creating and monitoring solar projects, predicting energy production using machine learning, real-time chat between users, and sending notifications.

---

## 🚀 Features
- **Dashboard** – overview of projects, tasks, and key information  
- **Project Management** – create, view, and manage project details  
- **Chat System** – communication between users with the ability to send documents and images  
- **Notifications** – real-time notifications via Firebase  
- **Prediction** – machine learning for predicting annual energy production based on solar system data  

---

## 🛠️ Technologies

### Frontend (Flutter)
- Built with **Flutter** using the **Dart** programming language  
- Cross-platform support (desktop and mobile applications)  
- **Hot Reload** for quick testing of changes  
- UI based on **widgets** → intuitive and adaptable design  

### Backend (.NET + REST API)
- Implemented with **.NET Framework** using **REST API** architecture  
- Scalability and security (authentication and authorization mechanisms)  
- Modular structure for easier maintenance and future upgrades  

### Database (SQL)
- **SQL** database for secure and structured data storage  
- Referential integrity with primary and foreign keys  
- Scalability and performance optimization  

### Firebase (Chat and Notifications)
- **Firebase Messaging** for push notifications  
- **Firestore** for real-time message storage  
- Simple and intuitive chat interface  

### Machine Learning (Python)
- Algorithms: **Random Forest, Decision Trees, Neural Networks**  
- Models trained and tested in **Python**  
- Integrated into the .NET backend via REST API  

---

## 📊 Solar Energy Prediction in Bosnia and Herzegovina
- Data collected from public sources about solar power plants and projects in BiH  
- Factors: **location (geo latitude & longitude)**, **GHI**, **air temperature**, **system size (kW/MW)**  
- The model predicts annual energy production and provides a ranking of the most suitable locations for new projects  
- Results are stored in `.csv` and `.txt` files with ranked location lists  

---

## 🌍 Solar Energy Potential in Bosnia and Herzegovina
- Theoretical potential: **74.64 million GWh**  
- Northern BiH: ~1240 kWh/m² annually  
- Southern BiH: ~1600 kWh/m² annually  
- BiH is considered one of the more favorable locations in Europe for solar power development  

---

## 📂 Project Structure
- **/frontend** – Flutter application (UI, dashboard, chat, notifications)  
- **/backend** – .NET REST API and database integration  
- **/ml** – Python machine learning models and predictions  
- **/database** – SQL scripts and database structure  

---
