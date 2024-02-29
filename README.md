This project has the following 3 endpoints,

Get candidate API
http://localhost:5228/api/BEAPI/candidate

Get location API
http://localhost:5228/api/BEAPI/location/{ipaddress}

Get passangers and total API
http://localhost:5228/api/BEAPI/listings/{numberofpassangers}

Quick walkthrough on the projects,

BECodingChallenge - This project contains the BEAPIController which is the main controller that has the candidate, location/{ipaddress} and listings/{numberofpassangers} endpoints.

BEBusinessService - This project contains the services consumed by BEAPIController.

Models - This project contains the Models.

BECodingChallengeTest - This project contains the unit test for the controller and the services.
