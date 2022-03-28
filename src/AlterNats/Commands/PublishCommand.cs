﻿using System.Threading.Tasks.Sources;

namespace AlterNats.Commands;

internal sealed class PublishCommand<T> : CommandBase<PublishCommand<T>>
{
    NatsKey? subject;
    T? value;
    INatsSerializer? serializer;

    PublishCommand()
    {
    }

    // TODO:reply-to

    public static PublishCommand<T> Create(string subject, T? value, INatsSerializer serializer)
    {
        if (!pool.TryPop(out var result))
        {
            result = new PublishCommand<T>();
        }

        result.subject = new NatsKey(subject); // TODO:use specified overload.
        result.value = value;
        result.serializer = serializer;

        return result;
    }

    public static PublishCommand<T> Create(NatsKey subject, T? value, INatsSerializer serializer)
    {
        if (!pool.TryPop(out var result))
        {
            result = new PublishCommand<T>();
        }

        result.subject = subject;
        result.value = value;
        result.serializer = serializer;

        return result;
    }

    public override void Write(ProtocolWriter writer)
    {
        writer.WritePublish(subject!, null, value, serializer!);
    }

    public override void Reset()
    {
        subject = null;
        value = default;
        serializer = null;
    }
}

internal sealed class AsyncPublishCommand<T> : AsyncCommandBase<AsyncPublishCommand<T>>
{
    NatsKey? subject;
    T? value;
    INatsSerializer? serializer;

    AsyncPublishCommand()
    {
    }

    // TODO:reply-to

    public static AsyncPublishCommand<T> Create(string subject, T? value, INatsSerializer serializer)
    {
        if (!pool.TryPop(out var result))
        {
            result = new AsyncPublishCommand<T>();
        }

        result.subject = new NatsKey(subject); // TODO:use specified overload.
        result.value = value;
        result.serializer = serializer;

        return result;
    }

    public static AsyncPublishCommand<T> Create(NatsKey subject, T? value, INatsSerializer serializer)
    {
        if (!pool.TryPop(out var result))
        {
            result = new AsyncPublishCommand<T>();
        }

        result.subject = subject;
        result.value = value;
        result.serializer = serializer;

        return result;
    }

    public override void Write(ProtocolWriter writer)
    {
        writer.WritePublish(subject!, null, value, serializer!);
    }

    public override void Reset()
    {
        subject = null;
        value = default;
        serializer = null;
    }
}



// TODO:Async Impl
internal sealed class PublishRawCommand : CommandBase<PublishRawCommand>
{
    NatsKey? subject;
    ReadOnlyMemory<byte> value;

    PublishRawCommand()
    {
    }

    // TODO:reply-to
    // TODO:ReadOnlyMemory<byte> overload

    public static PublishRawCommand Create(string subject, byte[] value)
    {
        if (!pool.TryPop(out var result))
        {
            result = new PublishRawCommand();
        }

        result.subject = new NatsKey(subject); // TODO:use specified overload.
        result.value = value;

        return result;
    }

    public static PublishRawCommand Create(NatsKey subject, byte[] value, INatsSerializer serializer)
    {
        if (!pool.TryPop(out var result))
        {
            result = new PublishRawCommand();
        }

        result.subject = subject;
        result.value = value;

        return result;
    }

    public override void Write(ProtocolWriter writer)
    {
        writer.WritePublish(subject!, null, value.Span);
    }

    public override void Reset()
    {
        subject = null;
        value = default;
    }
}
