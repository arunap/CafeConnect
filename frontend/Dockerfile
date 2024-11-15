# Step 1: Use an official Node.js runtime as a parent image
FROM node:18-alpine

# Step 2: Set the working directory in the container
WORKDIR /app

# Step 3: Copy the package.json and package-lock.json to the working directory
COPY package*.json ./

# Step 4: Install dependencies
RUN npm install

# Step 5: Copy the rest of the application code to the working directory
COPY . .

# Step 6: Build the React application
RUN npm run build

# Step 7: Install serve to serve the application
RUN npm install -g serve

# Step 8: Expose the port the app will run on
EXPOSE 3000

# Step 9: Start the app using serve
CMD ["serve", "-s", "build", "-l", "3000"]