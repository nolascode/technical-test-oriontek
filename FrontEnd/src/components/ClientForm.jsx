/* eslint-disable react/prop-types */
import { useFormik } from 'formik';
const ClientForm = ({
  onSubmit = null,
  cancelSumbit,
  clientSelectedToWork,
}) => {
  const formik = useFormik({
    initialValues: {
      name: clientSelectedToWork?.name ?? '',
      email: clientSelectedToWork?.email ?? '',
      phone: clientSelectedToWork?.phone ?? '',
    },
    onSubmit: (values) => {
      if (onSubmit !== null) {
        onSubmit(values);
      }
    },
  });

  return (
    <div className="mt-2 px-3">
      <form onSubmit={formik.handleSubmit}>
        <div>
          <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
            Name
          </label>
          <input
            name="name"
            type="text"
            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
            placeholder="John"
            required
            onChange={formik.handleChange}
            value={formik.values.name}
          />
        </div>

        <div>
          <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
            Email
          </label>
          <input
            name="email"
            type="email"
            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
            placeholder="John"
            required
            onChange={formik.handleChange}
            value={formik.values.email}
          />
        </div>

        <div>
          <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
            Phone
          </label>
          <input
            name="phone"
            type="phone"
            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
            placeholder="John"
            required
            onChange={formik.handleChange}
            value={formik.values.phone}
          />
        </div>
        <div className="flex items-center justify-center mt-2">
          <button
            onClick={(e) => {
              e.preventDefault();
              cancelSumbit();
            }}
            className="text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-red-300 font-medium rounded-lg text-sm px-5 py-1 mr-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700"
          >
            Cancel
          </button>
          <button
            className="text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-green-300 font-medium rounded-lg text-sm px-5 py-1 mr-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700"
            type="submit"
          >
            Submit
          </button>
        </div>
      </form>
    </div>
  );
};

export default ClientForm;
