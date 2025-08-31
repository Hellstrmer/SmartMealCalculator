export const get = async (url, params = {}) => {
    const queryString = new URLSearchParams(params).toString();
    const response = await fetch(`${url}?${queryString}`);
    
    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Network response was not ok');
    }
  
    return response.json();
  };
  
//   export const post = async (url, data) => {
//
//   };