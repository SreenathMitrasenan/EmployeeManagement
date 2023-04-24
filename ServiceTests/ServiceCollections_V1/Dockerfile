# Use the official Node.js 14 image as the base image
FROM postman/newman:alpine

# Set the working directory to /app
WORKDIR /app

# Install the necessary reporters
RUN npm install -g newman-reporter-html newman-reporter-htmlextra

# Copy the collection and data files into the container at /app
COPY . /app

# Run the tests when the container starts
ENTRYPOINT  ["newman", "run", "EMSmokeServices.json","-d","TestData.csv"]


# Command to run the docker file 
# docker run --rm --network=host -v <pwd>:/app <image> newman run EMSmokeServices.json -d TestData.csv  -r htmlextra


