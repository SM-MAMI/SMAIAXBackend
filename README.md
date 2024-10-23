# smaiax-backend

[![Build](https://github.com/SM-MAMI/smaiax-backend/actions/workflows/ci.yml/badge.svg)](https://github.com/SM-MAMI/smaiax-backend/actions/workflows/ci.yml)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SM-MAMI_SMAIAXBackend&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=SM-MAMI_SMAIAXBackend)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=SM-MAMI_SMAIAXBackend&metric=coverage)](https://sonarcloud.io/summary/new_code?id=SM-MAMI_SMAIAXBackend)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=SM-MAMI_SMAIAXBackend&metric=bugs)](https://sonarcloud.io/summary/new_code?id=SM-MAMI_SMAIAXBackend)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=SM-MAMI_SMAIAXBackend&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=SM-MAMI_SMAIAXBackend)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=SM-MAMI_SMAIAXBackend&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=SM-MAMI_SMAIAXBackend)

## Domain Model
```mermaid
classDiagram
    direction LR

    namespace UserAggregate {
        class User {
            <<AggregateRoot>>
            id
            name
            email
        }
}

    namespace SmartMeterAggregate {
        class SmartMeter {
            <<AggregateRoot>>
            id
            name
        }

        class Metadata {
            id
            validFrom
            location
            householdSize
        }
}

    namespace MeasurementAggregate {
        class Measurement {
            <<AggregateRoot>>
            id
            timestamp
            measurementData
        }
    }

    namespace PolicyAggregate {
        class Policy {
            <<AggregateRoot>>
            id
            measurementResolution
            location
            locationResolution
            price
        }
    }

    namespace PolicyRequestAggregate {
        class PolicyRequest {
            <<AggregateRoot>>
            id
            isAutomaticContractingEnabled
            policyFilter
        }

        class PolicyFilter {
            <<ValueObject>>
            measurementResolution
            householdSize
            locations
            locationResolution
            maxPrice
        }
    }

    namespace ContractAggregate {
        class Contract {
            <<AggregateRoot>>
            id
            createdAt
            policyCopy
            policyRequestCopy
        }
}

Measurement .. Metadata : matching by timestamp
Contract "0..*" -- "1" Policy : originates from copy
Contract "0..*" -- "1" PolicyRequest : originates from copy
Policy "0..*" -- "1" User : creates
    PolicyRequest -- PolicyFilter
PolicyRequest "0..*" -- "1" User : creates
User "1" -- "0..*" SmartMeter : owns
SmartMeter "0..*" -- "0..*" Policy : refers to
SmartMeter "1" -- "0..*" Measurement : produces
SmartMeter "1" -- "0..*" Metadata : is enriched with
```