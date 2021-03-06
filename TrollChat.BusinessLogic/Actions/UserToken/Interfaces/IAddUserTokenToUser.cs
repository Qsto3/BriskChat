﻿using System;
using TrollChat.BusinessLogic.Actions.Base;

namespace TrollChat.BusinessLogic.Actions.UserToken.Interfaces
{
    public interface IAddUserTokenToUser : IAction
    {
        string Invoke(Guid userId);
    }
}