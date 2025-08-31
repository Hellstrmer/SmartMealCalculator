// import { get } from './apiClient.js';

// const API_BASE_URL = 'https://localhost:5099';


// //const API_BASE_URL = 'http://127.0.0.1:8000/compare';
// const loader = document.getElementById("loading");
// const rP_loader = document.getElementById("RP_loading");


// const displayLoading = (loader) => {
    
//     loader.classList.add("display");
//     setTimeout(() => {
//         loader.classList.remove("display");
//     }, 30000);
// }

// const hideLoading = (loader) => {
//     loader.classList.remove("display");
// }

// export const GetIngredientsAsync = async () => {
//     try {
//         const response = await fetch(`${API_BASE_URL}/api/Ingredients`)

//         if (response == null) {
            
//         }
//     }
// }

// export const fetchFastestLap = async (year, sessionName, sessionType, lapnumberD1, lapnumberD2, drivers) => {
//     try {
//         const params = new URLSearchParams({
//             year: year,
//             sessionname: sessionName,
//             sessiontype: sessionType,
//             lapnumberd1: lapnumberD1,
//             lapnumberd2: lapnumberD2
//         });        
    
//         displayLoading(loader);
        
//         drivers.forEach(driver => {
//             params.append('drivers', driver);
//         });

//         const response = await fetch(`${API_BASE_URL}/fastestlap?${params.toString()}`);
        
//         if (!response.ok) {
//             hideLoading(loader);
//             const errorData = await response.json().catch(() => null);
//             throw new Error(errorData?.message || `HTTP error! status: ${response.status}`);
//         }
//         if (response.ok)            
//             hideLoading(loader);

//         const data = await response.json();
        
//         if (!data || !data.Message) {
//             throw new Error("Invalid data structure received");
//         }

//         return data.Message;
//     } catch (error) {
//         console.error("Error in fetchFastestLap:", error);
//         throw error;
//     }
// };
  
//   export const fetchRacePace = async (year, sessionName, sessionType) => {
//     try {
//         const params = new URLSearchParams({
//             year: year,
//             sessionname: sessionName,
//             sessiontype: sessionType
//         });
        
//         displayLoading(rP_loader);        
//         const response = await fetch(`${API_BASE_URL}/racepace?${params.toString()}`);        
//         if (!response.ok) {
//             const errorData = await response.json().catch(() => null);
//             hideLoading(rP_loader);
//             throw new Error(errorData?.message || `HTTP error! status: ${response.status}`);            
//         }
//         if (response.ok)            
//             hideLoading(rP_loader);

//         const data = await response.json();
        
//         if (!data || !data.Message) {
//             hideLoading(rP_loader);
//             throw new Error("Invalid data structure received");
//         }
//         return data.Message;
//     } catch (error) {
//         console.error("Error in fetchRacePace:", error);
//         hideLoading(rP_loader);
//         throw error;
//     }
//   };
// export const fetchRacePaceYear = async () => {
//     try { 
//         displayLoading(rP_loader);
        
//         const response = await fetch(`${API_BASE_URL}/racepaceyear`);
        
//         if (!response.ok) {
//             const errorData = await response.json().catch(() => null);
//             hideLoading(rP_loader);
//             throw new Error(errorData?.message || `HTTP error! status: ${response.status}`);            
//         }
//         if (response.ok)            
//             hideLoading(rP_loader);

//         const data = await response.json();
        
//         if (!data || !data.Message) {
//             hideLoading(rP_loader);
//             throw new Error("Invalid data structure received");
//         }
//         return data.Message;
//     } catch (error) {
//         console.error("Error in fetchRacePaceyear:", error);
//         hideLoading(rP_loader);
//         throw error;
//     }
//   };


//   export const fetchRaceweekend = async (year) => {
//     try {
//         const params = new URLSearchParams({
//             year: year
//         });
        
//         const response = await fetch(`${API_BASE_URL}/raceweekends?${params.toString()}`);
        
//         if (!response.ok) {
//             const errorData = await response.json().catch(() => null);
//             throw new Error(errorData?.message || `HTTP error! status: ${response.status}`);
//         }

//         const data = await response.json();
        
//         if (!data || !data.Message) {
//             throw new Error("Invalid data structure received");
//         }

//         return data.Message;
//     } catch (error) {
//         console.error("Error in fetchRaceweekend:", error);
//         throw error;
//     }
//   };

//   export const fetchNumberOfLaps = async (race) => {
//     try {
//         const params = new URLSearchParams({
//             race: race
//         });
        
//         const response = await fetch(`${API_BASE_URL}/numberoflaps?${params.toString()}`);
        
//         if (!response.ok) {
//             const errorData = await response.json().catch(() => null);
//             throw new Error(errorData?.message || `HTTP error! status: ${response.status}`);
//         }

//         const data = await response.json();
        
//         if (!data || !data.Message) {
//             throw new Error("Invalid data structure received");
//         }

//         return data.Message;
//     } catch (error) {
//         console.error("Error in fetchNumberOfLaps:", error);
//         throw error;
//     }
//   };

//   export const fetchDriverData = async (year, sessionName, sessionType) => {
//     try {
//         const params = new URLSearchParams({
//             year: year,
//             sessionname: sessionName,
//             sessiontype: sessionType
//         });
        
//         displayLoading(rP_loader);        
//         const response = await fetch(`${API_BASE_URL}/driverdata?${params.toString()}`); 

//         if (!response.ok) {
//             const errorData = await response.json().catch(() => null);
//             hideLoading(rP_loader);
//             throw new Error(errorData?.message || `HTTP error! status: ${response.status}`);            
//         }
//         if (response.ok)            
//             hideLoading(rP_loader);

//         const data = await response.json();
//         console.log("Data:")
//         console.log(data)
        
//         if (!data || !data.Message) {
//             hideLoading(rP_loader);
//             throw new Error("Invalid data structure received");
//         }
//         return data.Message;
//     } catch (error) {
//         console.error("Error in fetchDriverData:", error);
//         hideLoading(rP_loader);
//         throw error;
//     }
//   }


