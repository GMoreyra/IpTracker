# IpTracker

IpTracker it´s a web app that returns information about the ip.

## Installation

Use docker to install ipTracker

```bash
docker-compose build
```

## Usage

```
# returns the ip information
iptracker/{ip}

# returns the most requested ip information from the farest and closest invocations based on the distance to Buenos Aires 
iptracker/statistic

# returns the average distance to Buenos Aires from the max and min statistic
iptracker/average
```
