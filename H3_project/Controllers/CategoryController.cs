namespace H3_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository temp)
        {
            categoryRepository = temp;
        }

        // GetAll method:
        [HttpGet]
        public async Task<ActionResult> GetAllCategoryType()
        {
            try
            {
                var category = await categoryRepository.GetCategory();

                if (category == null)
                {
                    return Problem("Nothing was returned from category service, this is unexpected");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        // Get by id Method
        [HttpGet("{categoryId}")]

        public async Task<ActionResult> GetCategoryById(int categoryId)
        {
            try
            {
                var category = await categoryRepository.GetCategoryById(categoryId);

                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{categoryId}")]

        public async Task<ActionResult> PutCategory(Category category, int categoryId)
        {
            try
            {
                var categoryResult = await categoryRepository.UpdateCategory(categoryId, category);

                if (category == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(category);
        }

        // Create Method
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategories(Category category)
        {
            try
            {
                var createCategory = await categoryRepository.CreateCategory(category);

                if (createCategory == null)
                {
                    return StatusCode(500, "Category was not created. Something failed...");
                }
                return CreatedAtAction("PostCategories", new { categoryId = createCategory.CategoryID }, createCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the UserType {ex.Message}");
            }
        }




        // Delete Method
        [HttpDelete("{categoryId}")]

        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                var category = await categoryRepository.DeleteCategory(categoryId);

                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
