import AddressContainer from './components/AddressContainer.jsx';
import ClientContainer from './components/ClientContainer.jsx';
import CompanyContainer from './components/CompanyContainer.jsx';
import { useEffect } from 'react';
import useCompany from './utils/hooks/useCompany.js';
function App() {
  const { dispatchGetCompaniesListAsync } = useCompany();
  useEffect(() => {
    dispatchGetCompaniesListAsync();
  }, []);
  return (
    <div className="m-5 ">
      <div className="flex flex-row justify-around h-[97vh] w-full">
        <CompanyContainer />
        <ClientContainer />
        <AddressContainer />
      </div>
    </div>
  );
}

export default App;
