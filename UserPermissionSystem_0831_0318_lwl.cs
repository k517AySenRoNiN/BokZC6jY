// 代码生成时间: 2025-08-31 03:18:03
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// 控制器基类，包含权限验证
[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    protected void HandleException(Exception ex)
    {
        // 异常处理逻辑，可以根据实际需要调整
        Response.StatusCode = 500;
        Response.Headers.Add("Content-Type", "application/json");
        Response.WriteAsJson(new { success = false, message = ex.Message });
    }
}

// 用户权限管理系统控制器
[Authorize]
public class PermissionController : BaseController
{
    private readonly IUserPermissionService _permissionService;

    public PermissionController(IUserPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // 获取用户权限列表
    [HttpGet]
    public IActionResult GetUserPermissions(string userId)
    {
        try
        {
            var permissions = _permissionService.GetUserPermissions(userId);
            if (permissions == null)
            {
                return NotFound("User not found");
            }
            return Ok(permissions);
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return StatusCode(500);
        }
    }

    // 添加用户权限
    [HttpPost]
    public IActionResult AddUserPermission(string userId, string permission)
    {
        try
        {
            var result = _permissionService.AddUserPermission(userId, permission);
            if (!result)
            {
                return BadRequest("Permission already exists");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return StatusCode(500);
        }
    }
}

// 用户权限服务接口
public interface IUserPermissionService
{
    IEnumerable<string> GetUserPermissions(string userId);
    bool AddUserPermission(string userId, string permission);
}

// 用户权限服务实现类
public class UserPermissionService : IUserPermissionService
{
    private readonly Dictionary<string, HashSet<string>> _userPermissions;

    public UserPermissionService()
    {
        _userPermissions = new Dictionary<string, HashSet<string>();
    }

    public IEnumerable<string> GetUserPermissions(string userId)
    {
        if (!_userPermissions.ContainsKey(userId))
        {
            return Array.Empty<string>();
        }
        return _userPermissions[userId];
    }

    public bool AddUserPermission(string userId, string permission)
    {
        if (!_userPermissions.ContainsKey(userId))
        {
            _userPermissions[userId] = new HashSet<string>();
        }
        if (_userPermissions[userId].Contains(permission))
        {
            return false;
        }
        _userPermissions[userId].Add(permission);
        return true;
    }
}
