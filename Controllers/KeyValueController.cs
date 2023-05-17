using Assignment_1_KeyValuePairs.DBContext;
using Assignment_1_KeyValuePairs.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Assignment_1_KeyValuePairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyValueController : ControllerBase
    {
        private readonly KeyValueDBContext _dbContext;
        public KeyValueController(KeyValueDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<KeyValue>>> Get()
        {
            if (_dbContext.KeyValuePairs == null)
            {
                return NotFound();
            }
            return Ok(await _dbContext.KeyValuePairs.ToListAsync());
        }

        [HttpGet("/keys/{key}")]
        public async Task<ActionResult<List<KeyValue>>> GetKey(string key)
        {
            var keyValue = _dbContext.KeyValuePairs.FirstOrDefault(kv => kv.Key == key);
            if (keyValue != null)
            {
                return Ok(keyValue);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("/keys")]
        public async Task<ActionResult<List<KeyValue>>> AddKeyValue(KeyValue keyValue)
        {
            var _keyValue = _dbContext.KeyValuePairs.FirstOrDefault(kv => kv.Key == keyValue.Key);
            if (_keyValue != null)
            {
                return Conflict();
            }
            else
            {
                _dbContext.KeyValuePairs.Add(keyValue);
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.KeyValuePairs.ToListAsync());
            }
        }


        [HttpPut("/keys")]
        public async Task<ActionResult<List<KeyValue>>> UpdateKeyValue(KeyValue keyValue)
        {
            var _keyValue = _dbContext.KeyValuePairs.FirstOrDefault(kv => kv.Key == keyValue.Key);
            if (_keyValue != null)
            {
                _keyValue.Value = keyValue.Value;
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.KeyValuePairs.ToListAsync());
            }
            else
            {
                _dbContext.KeyValuePairs.Add(keyValue);
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.KeyValuePairs.ToListAsync());
            }
        }


        [HttpPatch("/keys/{key}/{value}")]
        public async Task<ActionResult<List<KeyValue>>> UpdateValue(string key, string value)
        {
            var _keyValue = _dbContext.KeyValuePairs.FirstOrDefault(kv => kv.Key == key);
            if (_keyValue != null)
            {
                _keyValue.Value = value;
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.KeyValuePairs.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("/keys/{key}")]
        public async Task<ActionResult<List<KeyValue>>> Delete(string key)
        {
            var keyValue = _dbContext.KeyValuePairs.FirstOrDefault(kv => kv.Key == key);
            if (keyValue != null)
            {
                _dbContext.KeyValuePairs.Remove(keyValue);
                await _dbContext.SaveChangesAsync();
                return Ok(await _dbContext.KeyValuePairs.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }


    }
}
