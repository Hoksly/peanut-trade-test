# CryptoExchangeAPI

CryptoExchangeAPI is a service that provides two main endpoints for cryptocurrency exchange rate information. It supports two centralized exchanges, Binance and Kucoin, and three cryptocurrencies: ETH, BTC, and USDT.

## Endpoints

### 1. Estimate

This endpoint determines the most profitable exchange for a given cryptocurrency transaction.

#### Parameters

- `inputAmount`: The amount of cryptocurrency to be exchanged.
- `inputCurrency`: The type of cryptocurrency to be exchanged.
- `outputCurrency`: The type of cryptocurrency to be received.

#### Response

- `exchangeName`: The name of the most profitable exchange (Binance or Kucoin).
- `outputAmount`: The amount of output currency to be received.

#### Example

Given that the value of BTC on Binance is 11000 USDT and on Kucoin is 10000 USDT, the service should respond to the request `inputAmount: 0.5, inputCurrency: "BTC", outputCurrency: "USDT"` with `exchangeName: Binance, outputAmount: 5500`.

### 2. GetRates

This endpoint returns the prices of a specified cryptocurrency on all supported exchanges.

#### Parameters

- `baseCurrency`: The type of cryptocurrency to be priced.
- `quoteCurrency`: The type of currency to be used as the reference.

#### Response

An array of objects, each containing:
- `exchangeName`: The name of the exchange.
- `rate`: The price of 1 `baseCurrency` in `quoteCurrency` on the specified exchange.

#### Example

On request `baseCurrency: BTC, quoteCurrency: ETH`, the service must respond with `{ [ { exchangeName: Binance, rate: 10 }, { exchangeName: Kucoin, rate: 8 }]}`.

## APIs

The service works with the APIs of the supported exchanges:

- [Binance API](https://binance-docs.github.io/apidocs/spot/en/#change-log)
- [Kucoin API](https://docs.kucoin.com/#general)

## Extensibility

The service architecture allows for the rapid addition of new exchanges and cryptocurrencies.