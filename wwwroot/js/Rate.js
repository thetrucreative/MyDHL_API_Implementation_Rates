document.getElementById('rateForm').addEventListener('submit', async function (event)
{
    console.log("JavaScript is running");
    event.preventDefault();  // Prevent the form from submitting traditionally
    const formData = new FormData(event.target);
    const queryParams = new URLSearchParams();
    formData.forEach((value, key) =>
    {
        if (key === 'isCustomsDeclarable')
        {
            value = document.getElementById(key).checked ? 'true' : 'false';
        }
        if (key === 'plannedShippingDate')
        {
            // Format the date to YYYY-MM-DD
            value = value.split('T')[0];
        }
        queryParams.append(key, value);
    });
    const apiUrl = `https://localhost:5001/api/rate/getDHLRates?${queryParams.toString()}`;

    try
    {
        const response = await fetch(apiUrl,
        {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Plugin-Name': 'MyDHL_API_Implementation',
                'Plugin-Version': '1.0',
                'Shipping-System-Platform-Name': 'MyDHL_API_DotNet_Core_Implementation',
                'Shipping-System-Platform-Version': '1.0',
                'Webstore-Platform-Name': 'MyDHL_API_Webstore',
                'Webstore-Platform-Version': '1.0',
                'Authorization': 'Basic ' + btoa('yourUsername:yourPassword') // Basic Auth header
            }
        });
        if (response.ok)
        {
            const result = await response.json();
            document.getElementById('result').textContent = JSON.stringify(result, null, 2); // Display the result
        } else
        {
            throw new Error('Failed to get rates');
        }
    } catch (error)
    {
        console.error(error);
        document.getElementById('result').textContent = 'Error: ' + error.message;
    }
});