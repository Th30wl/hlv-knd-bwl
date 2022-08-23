# Coding Challenge Assignment


## Running from docker

To run API in docker type: `docker-compose up`

Once container is running API documentation will be available at the following url: http://localhost:8654/swagger/index.html

To run tests in docker type: `docker-compose -f .\docker-compose.test.yml up`

This will output the results in **TestResults** subfolder of the repository root.

## Things that are still missing:

- Better unit tests for edge cases
- API Controller unit tests

## Note
I have extended the response object with `GameValid` property so that it contains info if the number of downed pins is valid or not. To me that seemed better than just throwing an exception in that case. Hope that is ok.
