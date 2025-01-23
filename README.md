# Basketball API - (or as I called it... FullCourtInsights)

This project is a partial wrapper around the API-Sports (NBA) API. It provides the user the ability to search for an NBA player's personal information given a name or player ID. The user can also search for the player's statistics for different games given an ID and year (ex: 2024).

This project incoporates JWT tokens with refresh tokens so the user can continuously access the protected routes of this API.

This project utilizes C#, ASP.NET, Entity Framework, and PostgreSQL to create, store, and retrieve users/refresh-tokens.

As a Backend only project, below is some samples of the API working...

Note: I created a user with credentials:
-  email: **c@c.com**
- password: **12345**

## This screenshot shows the JSON data that is sent to the login endpoint.
![Screenshot 2025-01-23 122921](https://github.com/user-attachments/assets/e7c7d08c-2063-4496-accd-6538340c8245)

## This shows the Token and Refresh Token Response I get from logging in. This would be used by the client.
![login-works](https://github.com/user-attachments/assets/a04714d6-6ce7-46d4-a4dd-addc68be613a)

## This shows an attempt at a call to the /api/players endpoint with no Authorization header.
![no-auth-header](https://github.com/user-attachments/assets/46e6083c-4658-4f2a-ae37-db37a11526d9)

## This screenshot shows the response from the server if no token is attached to the Authorization header.
![unauthorized-1](https://github.com/user-attachments/assets/fb81c819-3815-4e74-bb80-d8353d22aba0)

## This screenshot shows the addition of the token to the header (the token received by the login response).
![header-added](https://github.com/user-attachments/assets/4d25e7e2-7b5c-40b7-a87b-79e123b8871e)

## A successful response after token is provided.
![success-response](https://github.com/user-attachments/assets/38bab7ff-f74a-4a1e-9816-c05eccf6ce03)

## Calling /api/players/statistics route with same token.
![statistics-route](https://github.com/user-attachments/assets/2f079ea8-db6d-4426-8fff-2eafd69ee81a)

## Again, the response is successful with a valid token.
![statistics-route-success](https://github.com/user-attachments/assets/6972f053-82df-498c-984f-18ab286d7e4e)
