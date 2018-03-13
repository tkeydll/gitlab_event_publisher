# gitlab_event_publisher
Gitlab の過去の event をElasticsearch に投げるやつ。

## Usage

```
cd gitlab_event_publisher
dotnet run
```

## Under Docker Container

```
cd gitlab_event_publisher
docker run -it --rm -v $PWD:/app tkeydll/gitlab_event_publisher dotnet run
```